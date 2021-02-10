using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Helpers.Token;

namespace Project.Diana.WebApi.Features.User.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, IActionResult>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginRequestHandler(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);

            if (!result.Succeeded)
            {
                return new UnauthorizedResult();
            }

            var user = await _queryDispatcher.Dispatch<UserGetByUsernameQuery, ApplicationUser>(new UserGetByUsernameQuery(request.Username));

            var token = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            await _commandDispatcher.Dispatch(new RefreshTokenCreateCommand(refreshToken.ExpiresOn, refreshToken.Token, refreshToken.UserId));

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