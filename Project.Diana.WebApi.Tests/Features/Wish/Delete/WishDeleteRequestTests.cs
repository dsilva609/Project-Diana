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
        public void Request_Throws_If_User_Is_Missing(int wishID)
        {
            Action createWithMissingUser = () => new WishDeleteRequest(null, wishID);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_UserID_Is_Missing(int wishID)
        {
            var user = new ApplicationUser { Id = string.Empty };

            Action createWithUserMissingUserID = () => new WishDeleteRequest(user, wishID);

            createWithUserMissingUserID.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_WishID_Is_Default()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var testUser = fixture.Create<ApplicationUser>();

            Action createWithDefaultWishID = () => new WishDeleteRequest(testUser, 0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}