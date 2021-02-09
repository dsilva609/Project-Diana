using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Submission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Submission
{
    public class WishSubmissionRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(string apiID, string category, string imageUrl, ItemReference itemType, string notes, bool owned)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var testUser = fixture.Create<ApplicationUser>();

            Action createWithMissingTitle = () => new WishSubmissionRequest(apiID, category, imageUrl, itemType, notes, owned, string.Empty, testUser);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Missing(string apiID, string category, string imageUrl,
            ItemReference itemType, string notes, bool owned, string title)
        {
            Action createWithMissingUser = () => new WishSubmissionRequest(apiID, category, imageUrl, itemType, notes, owned, title, null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_UserID_Is_Missing(string apiID, string category, string imageUrl, ItemReference itemType, string notes, bool owned, string title)
        {
            Action createWithMissingUser = () => new WishSubmissionRequest(apiID, category, imageUrl, itemType, notes, owned, title, new ApplicationUser { Id = string.Empty });

            createWithMissingUser.Should().Throw<ArgumentException>();
        }
    }
}