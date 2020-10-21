using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumUpdate;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumUpdate
{
    public class AlbumUpdateRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Artist_Is_Missing(
            int albumId,
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
              int yearReleased,
              ApplicationUser user)
        {
            Action createWithMissingArtist = () => new AlbumUpdateRequest(
                albumId,
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
                user);

            createWithMissingArtist.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(
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
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingArtist = () => new AlbumUpdateRequest(
                0,
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
                user);

            createWithMissingArtist.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(
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
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingArtist = () => new AlbumUpdateRequest(
                -1,
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
                user);

            createWithMissingArtist.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
            int albumId,
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
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingTitle = () => new AlbumUpdateRequest(
                albumId,
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
                user);

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(
            int albumId,
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
            int yearReleased
            )
        {
            Action createWithMissingUserId = () => new AlbumUpdateRequest(
                albumId,
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
            int albumId,
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
            Action createWithNullUser = () => new AlbumUpdateRequest(
                albumId,
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
                null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}