using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Update;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Update
{
    public class WishUpdateRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_ID_Is_Missing(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            ApplicationUser user)
        {
            Action createWithMissingID = ()
                => new WishUpdateRequest(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    user,
                    0);

            createWithMissingID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            ApplicationUser user,
            int wishID)
        {
            Action createWithMissingTitle = ()
                => new WishUpdateRequest(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    string.Empty,
                    user,
                    wishID);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Missing(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            int wishID)
        {
            Action createWithMissingUser = ()
                => new WishUpdateRequest(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    null,
                    wishID);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_UserID_Is_Missing(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            int wishID)
        {
            Action createWithMissingUserID =
                () => new WishUpdateRequest(
                    apiID,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    new ApplicationUser
                    {
                        Id = string.Empty
                    },
                    wishID);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }
    }
}