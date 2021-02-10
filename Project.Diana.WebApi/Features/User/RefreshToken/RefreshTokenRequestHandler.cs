using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.User.Login;
using Project.Diana.WebApi.Helpers.Token;

namespace Project.Diana.WebApi.Features.User.RefreshToken
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ITokenService _tokenService;

        public RefreshTokenRequestHandler(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ITokenService tokenService)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var userResult = await _queryDispatcher.Dispatch<UserGetByRefreshTokenQuery, Result<ApplicationUser>>(new UserGetByRefreshTokenQuery(request.RefreshToken));

            if (userResult.IsFailure)
            {
                return new UnauthorizedResult();
            }

            var user = userResult.Value;

            var existingRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == request.RefreshToken);

            if (!existingRefreshToken.IsActive)
            {
                return new UnauthorizedResult();
            }

            var token = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            await _commandDispatcher.Dispatch(new RefreshTokenCreateCommand(refreshToken.ExpiresOn, refreshToken.Token, refreshToken.UserId));

            await _commandDispatcher.Dispatch(new RefreshTokenClearExpiredForUserCommand(existingRefreshToken.Token, existingRefreshToken.UserId));

            var loginResponse = new LoginResponse
            {
                DisplayName = user.DisplayName,
                RefreshToken = refreshToken.Token,
                Token = token,
                UserId = user.Id,
                UserNum = user.UserNum
            };

            return new OkObjectResult(loginResponse);
        }
    }
}