using System;
using System.Collections.Generic;
using MediatR;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.WebApi.Features.Book.SearchGoogleBooks
{
    public class SearchGoogleBooksRequest : IRequest<IEnumerable<BookSearchResponse>>
    {
        public string Author { get; }
        public string Title { get; }

        public SearchGoogleBooksRequest(string author, string title)
        {
            if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Author and title are missing", $"{nameof(author)}, {nameof(title)}");
            }

            Author = author;
            Title = title;
        }
    }
}