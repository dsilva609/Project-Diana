using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Commands
{
    public class BookUpdateCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Author_Is_Missing(
               int bookId,
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
            Action createWithMissingAuthor = () => new BookUpdateCommand(
                string.Empty,
                bookId,
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
        public void Command_Throws_If_Id_Is_Default(
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
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingAuthor = () => new BookUpdateCommand(
                author,
                0,
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
        public void Command_Throws_If_Id_Is_Negative(
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
            int yearReleased,
            ApplicationUser user)
        {
            Action createWithMissingAuthor = () => new BookUpdateCommand(
                author,
                -1,
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
        public void Command_Throws_If_Title_Is_Missing(
            string author,
            int bookId,
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
            Action createWithMissingTitle = () => new BookUpdateCommand(
                author,
                bookId,
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
        public void Command_Throws_If_User_Id_Is_Missing(
            string author,
            int bookId,
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
            Action createWithMissingUserId = () => new BookUpdateCommand(
                author,
                bookId,
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
        public void Command_Throws_If_User_Is_Null(
            string author,
            int bookId,
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
            Action createWithNullUser = () => new BookUpdateCommand(
                author,
                bookId,
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