using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Book.Queries;

namespace Project.Diana.WebApi.Features.Book.BookGetLatestAdded
{
    public class BookGetLatestAddedRequest : IRequest<BookListResponse>
    {
        public int ItemCount { get; }

        public BookGetLatestAddedRequest(int itemCount)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount;
        }
    }
}