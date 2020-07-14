using System;
using FluentAssertions;
using Project.Diana.Data.Features.User.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.User.Queries
{
    public class UserGetByUsernameQueryTests
    {
        [Fact]
        public void Query_Throws_If_Username_Is_Missing()
        {
            Action createWithMissingUsername = () => new UserGetByUsernameQuery(string.Empty);

            createWithMissingUsername.Should().Throw<ArgumentException>();
        }
    }
}