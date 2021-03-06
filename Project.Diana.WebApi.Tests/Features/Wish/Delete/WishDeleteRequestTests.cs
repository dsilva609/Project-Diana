using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Delete;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Delete
{
    public class WishDeleteRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Missing(int wishId)
        {
            Action createWithMissingUser = () => new WishDeleteRequest(null, wishId);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_UserId_Is_Missing(int wishId)
        {
            var user = new ApplicationUser { Id = string.Empty };

            Action createWithUserMissingUserId = () => new WishDeleteRequest(user, wishId);

            createWithUserMissingUserId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_WishId_Is_Default()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var testUser = fixture.Create<ApplicationUser>();

            Action createWithDefaultWishId = () => new WishDeleteRequest(testUser, 0);

            createWithDefaultWishId.Should().Throw<ArgumentException>();
        }
    }
}