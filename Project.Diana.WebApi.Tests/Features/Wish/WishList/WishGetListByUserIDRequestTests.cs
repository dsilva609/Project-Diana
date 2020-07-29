using System;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish;
using Project.Diana.WebApi.Features.Wish.WishList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.WishList
{
    public class WishGetListByUserIDRequestTests
    {
        [Fact]
        public void Request_Throws_When_User_Is_Missing()
        {
            Action createWithMissingUser = () => new WishGetListByUserIDRequest(null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_When_UserID_Is_Missing()
        {
            Action createWithMissingUserID = () => new WishGetListByUserIDRequest(new ApplicationUser { Id = string.Empty });

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }
    }
}