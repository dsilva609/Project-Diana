﻿using System;
using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookUpdate
{
    public class BookUpdateRequest : IRequest
    {
        public string Author { get; }
        public int BookId { get; set; }
        public string Category { get; }
        public CompletionStatusReference CompletionStatus { get; }
        public string CountryOfOrigin { get; }
        public string CountryPurchased { get; }
        public DateTime DatePurchased { get; }
        public string Genre { get; }
        public string ImageUrl { get; }
        public string ISBN10 { get; }
        public string ISBN13 { get; }
        public bool IsFirstEdition { get; }
        public bool IsHardcover { get; }
        public bool IsNewPurchase { get; }
        public bool IsPhysical { get; }
        public bool IsReissue { get; }
        public string Language { get; }
        public string LocationPurchased { get; }
        public string Notes { get; }
        public int PageCount { get; }
        public string Publisher { get; }
        public int TimesCompleted { get; }
        public string Title { get; }
        public BookTypeReference Type { get; }
        public ApplicationUser User { get; }
        public int YearReleased { get; }

        public BookUpdateRequest(
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
            int yearReleased,
            ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(bookId, nameof(bookId));
            Guard.Against.NullOrWhiteSpace(author, nameof(author));
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            Author = author;
            BookId = bookId;
            Category = category;
            CompletionStatus = completionStatus;
            CountryOfOrigin = countryOfOrigin;
            CountryPurchased = countryPurchased;
            DatePurchased = datePurchased;
            Genre = genre;
            ImageUrl = imageUrl;
            ISBN10 = isbn10;
            ISBN13 = isbn13;
            IsFirstEdition = isFirstEdition;
            IsHardcover = isHardcover;
            IsNewPurchase = isNewPurchase;
            IsPhysical = isPhysical;
            IsReissue = isReissue;
            Language = language;
            LocationPurchased = locationPurchased;
            Notes = notes;
            PageCount = pageCount;
            Publisher = publisher;
            TimesCompleted = timesCompleted;
            Title = title;
            Type = type;
            YearReleased = yearReleased;
            User = user;
        }
    }
}