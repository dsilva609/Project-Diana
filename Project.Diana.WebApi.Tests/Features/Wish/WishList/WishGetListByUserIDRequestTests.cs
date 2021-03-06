using System;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.WishList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.WishList
{
    public class WishGetListByUserIdRequestTests
    {
        [Fact]
        public void Request_Throws_When_User_Is_Missing()
        {
            Action createWithMissingUser = () => new WishGetListByUserIdRequest(null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_When_UserId_Is_Missing()
        {
            Action createWithMissingUserId = () => new WishGetListByUserIdRequest(new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}