﻿using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookUpdate;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookUpdate
{
    public class BookUpdateRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookUpdateRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Author_Is_Missing(
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
            Action createWithMissingAuthor = () => new BookUpdateRequest(
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
                _testUser);

            createWithMissingAuthor.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(
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
            Action createWithMissingAuthor = () => new BookUpdateRequest(
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
                _testUser);

            createWithMissingAuthor.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(
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
            Action createWithMissingAuthor = () => new BookUpdateRequest(
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
                _testUser);

            createWithMissingAuthor.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Title_Is_Missing(
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
            int yearReleased)
        {
            Action createWithMissingTitle = () => new BookUpdateRequest(
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
                _testUser);
            createWithMissingTitle.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(
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
            Action createWithMissingUserId = () => new BookUpdateRequest(
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
        public void Request_Throws_If_User_Is_Null(
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
            Action createWithNullUser = () => new BookUpdateRequest(
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
                null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}