using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Showcase.ShowcaseList;

namespace Project.Diana.WebApi.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequest : IRequest<ShowcaseListResponse>
    {
        public int UserId { get; }

        public ShowcaseGetListRequest(int userId)
        {
            Guard.Against.Default(userId, nameof(userId));

            UserId = userId;
        }
    }
}