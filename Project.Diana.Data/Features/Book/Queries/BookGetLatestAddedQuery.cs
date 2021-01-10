using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Book.Queries
{
    public class BookGetLatestAddedQuery : IQuery<BookListResponse>
    {
        public int ItemCount { get; }

        public BookGetLatestAddedQuery(int itemCount)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount == 0 ? 10 : itemCount;
        }
    }
}