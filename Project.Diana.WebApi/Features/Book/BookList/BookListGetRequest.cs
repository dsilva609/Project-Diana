using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookList
{
    public class BookListGetRequest : IRequest<BookListResponse>
    {
        public int ItemCount { get; }
        public int Page { get; }
        public string SearchQuery { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public BookListGetRequest(int itemCount, int page, string searchQuery, ApplicationUser user)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));
            Guard.Against.Negative(page, nameof(page));

            ItemCount = itemCount;
            Page = page;
            SearchQuery = searchQuery;
            User = user;
        }
    }
}