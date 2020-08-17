using System;
using FluentAssertions;
using Project.Diana.Data.Features.Showcase.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Showcase.Queries
{
    public class ShowcaseGetListQueryTests
    {
        [Fact]
        public void Query_Throws_If_User_ID_Is_Missing()
        {
            Action createWithMissingUserID = () => new ShowcaseGetListQuery(new ApplicationUser { Id = string.Empty });

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Query_Throws_If_User_Is_Missing()
        {
            Action createWithMissingUser = () => new ShowcaseGetListQuery(null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }
    }
}