using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetByIDQuery : IQuery<WishRecord>
    {
        public string UserID { get; }
        public int WishId { get; }

        public WishGetByIDQuery(string userID, int wishID)
        {
            Guard.Against.NullOrWhiteSpace(userID, nameof(userID));
            Guard.Against.Default(wishID, nameof(wishID));

            UserID = userID;
            WishId = wishID;
        }
    }
}