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

namespace Project.Diana.WebApi.Features.User
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, IActionResult>
    {
        private readonly GlobalSettings _settings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginRequestHandler(GlobalSettings settings, SignInManager<ApplicationUser> signInManager)
        {
            _settings = settings;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
            //--TODO: need to search for user
            if (result.Succeeded)
            {
                var token = GenerateToken();

                return new JsonResult(token);
            }

            return new UnauthorizedResult();
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user"),
                new Claim(JwtRegisteredClaimNames.Email, "email"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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