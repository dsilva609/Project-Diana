using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.User.RefreshToken;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.User.RefreshToken
{
    public class RefreshTokenRequestTests
    {
        [Fact]
        public void Request_Throws_When_Refresh_Token_Is_Missing()
        {
            Action createWithMissingRefreshToken = () => new RefreshTokenRequest(string.Empty);

            createWithMissingRefreshToken.Should().Throw<ArgumentException>();
        }
    }
}