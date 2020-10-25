using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookAddToShowcase
{
    public class BookAddToShowcaseRequest : IRequest
    {
        public int BookId { get; }
        public ApplicationUser User { get; }

        public BookAddToShowcaseRequest(int bookId, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(bookId, nameof(bookId));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            BookId = bookId;
            User = user;
        }
    }
}