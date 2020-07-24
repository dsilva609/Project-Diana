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
        public void Command_Throws_When_UserID_Is_Missing(int wishID)
        {
            Action createWithMissingUserID = () => new WishCompleteItemCommand(string.Empty, wishID);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_When_WishID_Is_Default(string userID)
        {
            Action createWithDefaultWishID = () => new WishCompleteItemCommand(userID, 0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}