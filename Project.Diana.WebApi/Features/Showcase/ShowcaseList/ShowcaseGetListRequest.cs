using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Showcase.ShowcaseList;

namespace Project.Diana.WebApi.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequest : IRequest<ShowcaseListResponse>
    {
        public int UserID { get; }

        public ShowcaseGetListRequest(int userID)
        {
            Guard.Against.Default(userID, nameof(userID));

            UserID = userID;
        }
    }
}