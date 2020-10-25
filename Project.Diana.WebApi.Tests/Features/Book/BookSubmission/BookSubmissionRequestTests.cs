using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookSubmission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookSubmission
{
    public class BookSubmissionRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Author_Is_Missing(
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            string genre,
            string imageUrl,
            string isbn10,
            string isbn13,
            bool isFirstEdition,
            bool isHardcover,
            bool isNewPurchase,
            bool isPhysical,
            bool isReissue,
            string language,
            string locationPurchased,
            string notes,
            int pageCount,
            string publisher,
            int timesCompleted,
            string title,
            BookTypeReference type,
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingAuthor = () => new BookSubmissionRequest(
                string.Empty,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                genre,
                imageUrl,
                isbn10,
                isbn13,
                isFirstEdition,
                isHardcover,
                isNewPurchase,
                isPhysical,
                isReissue,
                language,
                locationPurchased,
                notes,
                pageCount,
                publisher,
                timesCompleted,
                title,
                type,
                yearReleased,
                user);

            createWithMissingAuthor.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
            string author,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            string genre,
            string imageUrl,
            string isbn10,
            string isbn13,
            bool isFirstEdition,
            bool isHardcover,
            bool isNewPurchase,
            bool isPhysical,
            bool isReissue,
            string language,
            string locationPurchased,
            string notes,
            int pageCount,
            string publisher,
            int timesCompleted,
            BookTypeReference type,
            int yearReleased,
            ApplicationUser user
            )
        {
            Action createWithMissingTitle = () => new BookSubmissionRequest(
                author,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                genre,
                imageUrl,
                isbn10,
                isbn13,
                isFirstEdition,
                isHardcover,
                isNewPurchase,
                isPhysical,
                isReissue,
                language,
                locationPurchased,
                notes,
                pageCount,
                publisher,
                timesCompleted,
                string.Empty,
                type,
                yearReleased,
                user
                );

            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(
            string author,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            string genre,
            string imageUrl,
            string isbn10,
            string isbn13,
            bool isFirstEdition,
            bool isHardcover,
            bool isNewPurchase,
            bool isPhysical,
            bool isReissue,
            string language,
            string locationPurchased,
            string notes,
            int pageCount,
            string publisher,
            int timesCompleted,
            string title,
            BookTypeReference type,
            int yearReleased)
        {
            Action createWithMissingUserId = () => new BookSubmissionRequest(
                author,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                genre,
                imageUrl,
                isbn10,
                isbn13,
                isFirstEdition,
                isHardcover,
                isNewPurchase,
                isPhysical,
                isReissue,
                language,
                locationPurchased,
                notes,
                pageCount,
                publisher,
                timesCompleted,
                title,
                type,
                yearReleased,
                new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(
            string author,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            string genre,
            string imageUrl,
            string isbn10,
            string isbn13,
            bool isFirstEdition,
            bool isHardcover,
            bool isNewPurchase,
            bool isPhysical,
            bool isReissue,
            string language,
            string locationPurchased,
            string notes,
            int pageCount,
            string publisher,
            int timesCompleted,
            string title,
            BookTypeReference type,
            int yearReleased)
        {
            Action createWithNullUser = () => new BookSubmissionRequest(
                author,
                category,
                completionStatus,
                countryOfOrigin,
                countryPurchased,
                datePurchased,
                genre,
                imageUrl,
                isbn10,
                isbn13,
                isFirstEdition,
                isHardcover,
                isNewPurchase,
                isPhysical,
                isReissue,
                language,
                locationPurchased,
                notes,
                pageCount,
                publisher,
                timesCompleted,
                title,
                type,
                yearReleased,
                null
                );

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}