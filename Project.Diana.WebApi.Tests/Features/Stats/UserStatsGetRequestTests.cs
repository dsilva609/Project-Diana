using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Stats;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Stats
{
    public class UserStatsGetRequestTests
    {
        [Fact]
        public void Request_Throws_When_User_Id_Is_Default()
        {
            Action createWithDefaultUserId = () => new UserStatsGetRequest(0);

            createWithDefaultUserId.Should().Throw<ArgumentException>();
        }
    }
}