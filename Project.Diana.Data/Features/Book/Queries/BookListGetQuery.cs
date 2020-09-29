using System.Collections.Generic;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Book.Queries
{
    public class BookListGetQuery : IQuery<IEnumerable<BookRecord>>
    {
        public int ItemCount { get; }
        public ApplicationUser User { get; }

        public BookListGetQuery(int itemCount, ApplicationUser user)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount == 0 ? 10 : itemCount;
            User = user;
        }
    }
}