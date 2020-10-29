using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishDeleteCommand : ICommand
    {
        public int Id { get; }
        public ApplicationUser User { get; }

        public WishDeleteCommand(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            Id = id;
            User = user;
        }
    }
}