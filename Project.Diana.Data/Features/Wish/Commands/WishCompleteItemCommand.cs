using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishCompleteItemCommand : ICommand
    {
        public string UserID { get; }
        public int WishID { get; }

        public WishCompleteItemCommand(string userID, int wishID)
        {
            Guard.Against.NullOrWhiteSpace(userID, nameof(userID));
            Guard.Against.Default(wishID, nameof(wishID));

            UserID = userID;
            WishID = wishID;
        }
    }
}