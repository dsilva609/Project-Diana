using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishCompleteItemCommand : ICommand
    {
        public string UserId { get; }
        public int WishId { get; }

        public WishCompleteItemCommand(string userId, int wishId)
        {
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            Guard.Against.Default(wishId, nameof(wishId));

            UserId = userId;
            WishId = wishId;
        }
    }
}