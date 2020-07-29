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
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string userID,
            int wishID)
        {
            Action createWithMissingTitle = ()
                => new WishUpdateCommand(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    string.Empty,
                    userID,
                    wishID);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_UserID_Is_Missing(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title, int wishID)
        {
            Action createWithMissingUserID = ()
                => new WishUpdateCommand(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    string.Empty,
                    wishID);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_WishID_Is_Default(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            string userID)
        {
            Action createWithDefaultWishID = ()
                => new WishUpdateCommand(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    userID,
                    0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}