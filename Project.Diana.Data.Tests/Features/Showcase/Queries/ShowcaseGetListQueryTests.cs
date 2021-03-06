using System;
using FluentAssertions;
using Project.Diana.Data.Features.Showcase.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Showcase.Queries
{
    public class ShowcaseGetListQueryTests
    {
        [Fact]
        public void Query_Throws_If_User_Id_Is_Default()
        {
            Action createWithDefaultUserId = () => new ShowcaseGetListQuery(0);

            createWithDefaultUserId.Should().Throw<ArgumentException>();
        }
    }
}