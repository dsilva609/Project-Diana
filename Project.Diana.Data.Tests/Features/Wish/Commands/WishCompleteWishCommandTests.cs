using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Commands
{
    public class WishCompleteWishCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_When_UserId_Is_Missing(int wishId)
        {
            Action createWithMissingUserId = () => new WishCompleteItemCommand(string.Empty, wishId);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_When_WishId_Is_Default(string userId)
        {
            Action createWithDefaultWishId = () => new WishCompleteItemCommand(userId, 0);

            createWithDefaultWishId.Should().Throw<ArgumentException>();
        }
    }
}