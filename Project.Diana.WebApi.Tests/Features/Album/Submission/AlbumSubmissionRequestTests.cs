using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumSubmission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.Submission
{
    public class AlbumSubmissionRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumSubmissionRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Artist_Is_Missing(
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            int discogsId,
            string genre,
            string imageUrl,
            bool isNewPurchase,
            bool isPhysical,
            string locationPurchased,
            MediaTypeReference mediaType,
            string notes,
            string recordLabel,
            SizeReference size,
            SpeedReference speed,
            string style,
            int timesCompleted,
            string title,
            int yearReleased)
        {
            Action createWithMissingArtist = () => new AlbumSubmissionRequest(
                string.Empty,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                discogsId,
                genre,
                imageUrl,
                isNewPurchase,
                isPhysical,
                locationPurchased,
                mediaType,
                notes,
                recordLabel,
                size,
                speed,
                style,
                timesCompleted,
                title,
                yearReleased,
                _testUser);

            createWithMissingArtist.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
            string artist,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            int discogsId,
            string genre,
            string imageUrl,
            bool isNewPurchase,
            bool isPhysical,
            string locationPurchased,
            MediaTypeReference mediaType,
            string notes,
         string recordLabel,
            SizeReference size,
            SpeedReference speed,
            string style,
            int timesCompleted,
            int yearReleased)
        {
            Action createWithMissingTitle = () => new AlbumSubmissionRequest(
                artist,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                discogsId,
                genre,
                imageUrl,
                isNewPurchase,
                isPhysical,
                locationPurchased,
                mediaType,
                notes,
                recordLabel,
                size,
                speed,
                style,
                timesCompleted,
                string.Empty,
                yearReleased,
                _testUser);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(
            string artist,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            int discogsId,
            string genre,
            string imageUrl,
            bool isNewPurchase,
            bool isPhysical,
            string locationPurchased,
            MediaTypeReference mediaType,
            string notes,
            string recordLabel,
            SizeReference size,
            SpeedReference speed,
            string style,
            int timesCompleted,
            string title,
            int yearReleased)
        {
            Action createWithMissingUserId = () => new AlbumSubmissionRequest(
                artist,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                discogsId,
                genre,
                imageUrl,
                isNewPurchase,
                isPhysical,
                locationPurchased,
                mediaType,
                notes,
                recordLabel,
                size,
                speed,
                style,
                timesCompleted,
                title,
                yearReleased,
                new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(
            string artist,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            int discogsId,
            string genre,
            string imageUrl,
            bool isNewPurchase,
            bool isPhysical,
            string locationPurchased,
            MediaTypeReference mediaType,
            string notes,
            string recordLabel,
            SizeReference size,
            SpeedReference speed,
            string style,
            int timesCompleted,
            string title,
            int yearReleased)
        {
            Action createWithNullUser = () => new AlbumSubmissionRequest(artist,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                discogsId,
                genre,
                imageUrl,
                isNewPurchase,
                isPhysical,
                locationPurchased,
                mediaType,
                notes,
                recordLabel,
                size,
                speed,
                style,
                timesCompleted,
                title,
                yearReleased,
                null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}