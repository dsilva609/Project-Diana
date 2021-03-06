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
        public void Command_Throws_If_Title_Is_Missing(string apiId, string category, string imageUrl, ItemReference itemType, string notes, bool owned, string userId)
        {
            Action createWithMissingTitle = () => new WishCreateCommand(apiId, category, imageUrl, itemType, notes, owned, string.Empty, userId);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_UserId_Is_Missing(string apiId, string category, string imageUrl, ItemReference itemType, string notes, bool owned, string title)
        {
            Action createWithMissingUserId = () => new WishCreateCommand(apiId, category, imageUrl, itemType, notes, owned, title, string.Empty);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}