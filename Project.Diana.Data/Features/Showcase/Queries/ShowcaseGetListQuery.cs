using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;

namespace Project.Diana.Data.Features.Showcase.Queries
{
    public class ShowcaseGetListQuery : IQuery<ShowcaseListResponse>
    {
        public int UserID { get; }

        public ShowcaseGetListQuery(int userID)
        {
            Guard.Against.Default(userID, nameof(userID));

            UserID = userID;
        }
    }
}