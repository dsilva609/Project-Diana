using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Wish.Delete
{
    public class WishDeleteRequest : IRequest
    {
        public ApplicationUser User { get; }
        public int WishId { get; }

        public WishDeleteRequest(ApplicationUser user, int wishId)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));
            Guard.Against.Default(wishId, nameof(wishId));

            User = user;
            WishId = wishId;
        }
    }
}