using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Commands
{
    public class WishCreateCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Title_Is_Missing(string apiID, string category, string imageUrl, ItemReference itemType, string notes, bool owned, string userID)
        {
            Action createWithMissingTitle = () => new WishCreateCommand(apiID, category, imageUrl, itemType, notes, owned, string.Empty, userID);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_UserID_Is_Missing(string apiID, string category, string imageUrl, ItemReference itemType, string notes, bool owned, string title)
        {
            Action createWithMissingUserID = () => new WishCreateCommand(apiID, category, imageUrl, itemType, notes, owned, title, string.Empty);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }
    }
}