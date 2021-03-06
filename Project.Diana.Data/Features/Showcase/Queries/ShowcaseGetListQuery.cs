using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;

namespace Project.Diana.Data.Features.Showcase.Queries
{
    public class ShowcaseGetListQuery : IQuery<ShowcaseListResponse>
    {
        public int UserId { get; }

        public ShowcaseGetListQuery(int userId)
        {
            Guard.Against.Default(userId, nameof(userId));

            UserId = userId;
        }
    }
}