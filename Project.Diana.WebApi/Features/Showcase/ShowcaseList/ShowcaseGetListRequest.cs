using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Showcase.ShowcaseList;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequest : IRequest<ShowcaseListResponse>
    {
        public ApplicationUser User { get; }

        public ShowcaseGetListRequest(ApplicationUser user)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            User = user;
        }
    }
}