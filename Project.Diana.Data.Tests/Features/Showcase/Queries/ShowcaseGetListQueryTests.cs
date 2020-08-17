using System;
using FluentAssertions;
using Project.Diana.Data.Features.Showcase.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Showcase.Queries
{
    public class ShowcaseGetListQueryTests
    {
        [Fact]
        public void Query_Throws_If_User_ID_Is_Default()
        {
            Action createWithDefaultUserID = () => new ShowcaseGetListQuery(0);

            createWithDefaultUserID.Should().Throw<ArgumentException>();
        }
    }
}