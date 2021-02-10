using System;
using FluentAssertions;
using Project.Diana.Data.Features.User.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.User.Queries
{
    public class UserGetByRefreshTokenQueryTests
    {
        [Fact]
        public void Query_Throws_If_Refresh_Token_Is_Missing()
        {
            Action createWithMissingRefreshToken = () => new UserGetByRefreshTokenQuery(string.Empty);

            createWithMissingRefreshToken.Should().Throw<ArgumentException>();
        }
    }
}