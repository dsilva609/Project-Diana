using System;
using AutoFixture;
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
        private readonly ApplicationUser _testUser;

        public WishUpdateRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title)
        {
            Action createWithMissingId = ()
                => new WishUpdateRequest(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    _testUser,
                    0);

            createWithMissingId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            int wishId)
        {
            Action createWithMissingTitle = ()
                => new WishUpdateRequest(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    string.Empty,
                    _testUser,
                    wishId);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            int wishId)
        {
            Action createWithMissingUser = ()
                => new WishUpdateRequest(
                    apiId,
                    category,
                    imageUrl,
                    itemType,
                    notes,
                    owned,
                    title,
                    null,
                    wishId);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_UserId_Is_Missing(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            int wishId)
        {
            Action createWithMissingUserId =
                () => new WishUpdateRequest(
                    apiId,
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
                    wishId);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}