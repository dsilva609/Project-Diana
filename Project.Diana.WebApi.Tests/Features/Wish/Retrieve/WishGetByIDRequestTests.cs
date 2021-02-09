using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Retrieve;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Retrieve
{
    public class WishGetByIDRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_When_User_Is_Missing(int wishID)
        {
            Action createWithMissingUser = () => new WishGetByIDRequest(null, wishID);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_UserID_Is_Missing(int wishID)
        {
            Action createWithMissingUserID = () => new WishGetByIDRequest(new ApplicationUser { Id = string.Empty }, wishID);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_When_WishID_Is_Default()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var testUser = fixture.Create<ApplicationUser>();

            Action createWithDefaultId = () => new WishGetByIDRequest(testUser, 0);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}