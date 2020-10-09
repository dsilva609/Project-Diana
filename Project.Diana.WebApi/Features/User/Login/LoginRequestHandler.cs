using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.User.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, IActionResult>
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly GlobalSettings _settings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginRequestHandler(
            IQueryDispatcher queryDispatcher,
            GlobalSettings settings,
            SignInManager<ApplicationUser> signInManager)
        {
            _queryDispatcher = queryDispatcher;
            _settings = settings;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);

            if (result.Succeeded)
            {
                var user = await _queryDispatcher.Dispatch<UserGetByUsernameQuery, ApplicationUser>(new UserGetByUsernameQuery(request.Username));

                var token = GenerateToken(user);

                var loginResponse = new LoginResponse
                {
                    DisplayName = user.DisplayName,
                    Token = token,
                    UserId = user.Id
                };

                return new OkObjectResult(loginResponse);
            }

            return new UnauthorizedResult();
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
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    }
}