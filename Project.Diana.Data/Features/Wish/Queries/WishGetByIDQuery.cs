using Ardalis.GuardClauses;
using Kledex.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetByIDQuery : IQuery<string>
    {
        public int UserID { get; }
        public int WishId { get; }

        public WishGetByIDQuery(int userID, int wishID)
        {
            Guard.Against.Default(userID, nameof(userID));
            Guard.Against.Default(wishID, nameof(wishID));

            UserID = userID;
            WishId = wishID;
        }
    }
}