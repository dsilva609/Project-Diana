using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Helpers.Token
{
    public class TokenService : ITokenService
    {
        private readonly GlobalSettings _settings;

        public TokenService(GlobalSettings settings) => _settings = settings;

        public string GenerateAccessToken(ApplicationUser user)
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

        public RefreshTokenRecord GenerateRefreshToken(ApplicationUser user)
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
    }
}