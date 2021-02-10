using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.RefreshToken.Commands
{
    public class RefreshTokenCreateCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Expires_On_Is_Default(string token, string userId)
        {
            Action createWithDefaultExpiration = () => new RefreshTokenCreateCommand(default, token, userId);

            createWithDefaultExpiration.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Token_Is_Missing(DateTimeOffset expiresOn, string userId)
        {
            Action createWithMissingToken = () => new RefreshTokenCreateCommand(expiresOn, string.Empty, userId);

            createWithMissingToken.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(DateTimeOffset expiresOn, string token)
        {
            Action createWithMissingUserId = () => new RefreshTokenCreateCommand(expiresOn, token, string.Empty);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}