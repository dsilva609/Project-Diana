using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Queries
{
    public class WishGetByIdQueryTests
    {
        [Theory, AutoData]
        public void Query_Throws_When_UserId_Is_Missing(int wishId)
        {
            Action createWithDefaultUserId = () => new WishGetByIdQuery(string.Empty, wishId);

            createWithDefaultUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_When_WishId_Is_Default(string userId)
        {
            Action createWithDefaultWishId = () => new WishGetByIdQuery(userId, 0);

            createWithDefaultWishId.Should().Throw<ArgumentException>();
        }
    }
}