using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Retrieve;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Retrieve
{
    public class WishGetByIdRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_When_User_Is_Missing(int wishId)
        {
            Action createWithMissingUser = () => new WishGetByIdRequest(null, wishId);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_UserId_Is_Missing(int wishId)
        {
            Action createWithMissingUserId = () => new WishGetByIdRequest(new ApplicationUser { Id = string.Empty }, wishId);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_When_WishId_Is_Default()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var testUser = fixture.Create<ApplicationUser>();

            Action createWithDefaultId = () => new WishGetByIdRequest(testUser, 0);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}