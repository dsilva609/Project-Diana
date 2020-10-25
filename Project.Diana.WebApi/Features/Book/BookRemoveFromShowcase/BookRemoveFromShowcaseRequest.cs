using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase
{
    public class BookRemoveFromShowcaseRequest : IRequest
    {
        public int Id { get; }
        public ApplicationUser User { get; }

        public BookRemoveFromShowcaseRequest(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            Id = id;
            User = user;
        }
    }
}