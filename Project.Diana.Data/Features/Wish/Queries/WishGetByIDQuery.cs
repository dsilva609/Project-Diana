using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetByIdQuery : IQuery<WishRecord>
    {
        public string UserId { get; }
        public int WishId { get; }

        public WishGetByIdQuery(string userId, int wishId)
        {
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            Guard.Against.Default(wishId, nameof(wishId));

            UserId = userId;
            WishId = wishId;
        }
    }
}