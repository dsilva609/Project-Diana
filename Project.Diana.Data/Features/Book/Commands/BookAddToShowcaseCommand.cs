using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Book.Commands
{
    public class BookAddToShowcaseCommand : ICommand
    {
        public int BookId { get; }
        public ApplicationUser User { get; }

        public BookAddToShowcaseCommand(int bookId, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(bookId, nameof(bookId));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            BookId = bookId;
            User = user;
        }
    }
}