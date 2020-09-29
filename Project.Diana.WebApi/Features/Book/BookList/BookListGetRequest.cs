using System.Collections.Generic;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookList
{
    public class BookListGetRequest : IRequest<IEnumerable<BookRecord>>
    {
        public int ItemCount { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public BookListGetRequest(int itemCount, ApplicationUser user)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount;
            User = user;
        }
    }
}