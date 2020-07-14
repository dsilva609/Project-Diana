using System.Collections.Generic;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetListByUserIDQuery : IQuery<IEnumerable<WishRecord>>
    {
        public string UserID { get; }

        public WishGetListByUserIDQuery(string userID)
        {
            Guard.Against.NullOrWhiteSpace(userID, nameof(userID));

            UserID = userID;
        }
    }
}