using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Showcase.Queries
{
    public class ShowcaseGetListQuery : IQuery<ShowcaseListResponse>
    {
        public ApplicationUser User { get; }

        public ShowcaseGetListQuery(ApplicationUser user)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            User = user;
        }
    }
}