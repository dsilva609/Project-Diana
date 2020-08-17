using System;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequestTests
    {
        [Fact]
        public void Request_Throws_If_User_ID_Is_Missing()
        {
            Action createWithMissingUserID = () => new ShowcaseGetListRequest(new ApplicationUser { Id = string.Empty });

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_User_Is_Missing()
        {
            Action createWithMissingUser = () => new ShowcaseGetListRequest(null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }
    }
}