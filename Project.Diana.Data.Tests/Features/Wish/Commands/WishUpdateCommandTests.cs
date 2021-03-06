using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Commands
{
    public class WishUpdateCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Title_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string userId,
            int wishId)
        {
            Action createWithMissingTitle = ()
                => new WishUpdateCommand(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    string.Empty,
                    userId,
                    wishId);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_UserId_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title, int wishId)
        {
            Action createWithMissingUserId = ()
                => new WishUpdateCommand(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    string.Empty,
                    wishId);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_WishId_Is_Default(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            string userId)
        {
            Action createWithDefaultWishId = ()
                => new WishUpdateCommand(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    userId,
                    0);

            createWithDefaultWishId.Should().Throw<ArgumentException>();
        }
    }
}