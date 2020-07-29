using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Wish.WishList
{
    public class WishGetListByUserIDRequest : IRequest<WishListResponse>
    {
        public ApplicationUser User { get; }

        public WishGetListByUserIDRequest(ApplicationUser user)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            User = user;
        }
    }
}