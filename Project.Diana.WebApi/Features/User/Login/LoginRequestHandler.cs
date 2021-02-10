using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.User.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, IActionResult>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly GlobalSettings _settings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginRequestHandler(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            GlobalSettings settings,
            SignInManager<ApplicationUser> signInManager)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _settings = settings;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);

            if (!result.Succeeded)
            {
                return new UnauthorizedResult();
            }

            var user = await _queryDispatcher.Dispatch<UserGetByUsernameQuery, ApplicationUser>(new UserGetByUsernameQuery(request.Username));

            var token = GenerateToken(user);
            var refreshToken = GenerateRefreshToken(user);

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

        private RefreshTokenRecord GenerateRefreshToken(ApplicationUser user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return new RefreshTokenRecord
            {
                CreatedOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
                Token = Convert.ToBase64String(randomBytes),
                UserId = user.Id
            };
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),

                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                new Claim("UserNum", user.UserNum.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Issuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.TokenExpirationMinutes),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    }
}