using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.RefreshToken.Commands
{
    public class RefreshTokenClearExpiredForUserCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(string expiredToken)
        {
            Action createWithMissingUserId = () => new RefreshTokenClearExpiredForUserCommand(expiredToken, string.Empty);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}