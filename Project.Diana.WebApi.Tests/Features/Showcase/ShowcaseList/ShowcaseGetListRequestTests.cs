using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequestTests
    {
        [Fact]
        public void Request_Throws_If_User_ID_Is_Default()
        {
            Action createWithDefaultUserID = () => new ShowcaseGetListRequest(0);

            createWithDefaultUserID.Should().Throw<ArgumentException>();
        }
    }
}