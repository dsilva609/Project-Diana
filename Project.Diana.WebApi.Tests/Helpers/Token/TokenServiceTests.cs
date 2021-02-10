using System.IdentityModel.Tokens.Jwt;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Helpers.Token;
using Xunit;

namespace Project.Diana.WebApi.Tests.Helpers.Token
{
    public class TokenServiceTests
    {
        private readonly ApplicationUser _testUser;
        private readonly ITokenService _tokenService;

        public TokenServiceTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();

            var settings = fixture.Create<GlobalSettings>();

            _tokenService = new TokenService(settings);
        }

        [Fact]
        public void Generate_Access_Token_Returns_Token()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var result = _tokenService.GenerateAccessToken(_testUser);

            result.Should().NotBeNullOrWhiteSpace();

            var token = tokenHandler.ReadJwtToken(result);

            token.Should().NotBeNull();
            token.Subject.Should().Be(_testUser.Id);
        }

        [Fact]
        public void Generate_Refresh_Token_Returns_Token()
        {
            var result = _tokenService.GenerateRefreshToken(_testUser);

            result.Should().NotBeNull();
            result.UserId.Should().Be(_testUser.Id);
        }
    }
}