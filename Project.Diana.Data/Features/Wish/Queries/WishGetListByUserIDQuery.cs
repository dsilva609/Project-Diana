using System.Collections.Generic;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetListByUserIdQuery : IQuery<IEnumerable<WishRecord>>
    {
        public string UserId { get; }

        public WishGetListByUserIdQuery(string userId)
        {
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));

            UserId = userId;
        }
    }
}