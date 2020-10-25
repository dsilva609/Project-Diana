using System;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.Submission
{
    public class BookSubmission
    {
        public string Author { get; set; }
        public string Category { get; set; }
        public CompletionStatusReference CompletionStatus { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CountryPurchased { get; set; }
        public DateTime DatePurchased { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public bool IsFirstEdition { get; set; }
        public bool IsHardcover { get; set; }
        public bool IsNewPurchase { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsReissue { get; set; }
        public string Language { get; set; }
        public string LocationPurchased { get; set; }
        public string Notes { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public int ReadCount { get; set; }
        public string Title { get; set; }
        public BookTypeReference Type { get; set; }
        public ApplicationUser User { get; set; }
        public int YearReleased { get; set; }
    }
}