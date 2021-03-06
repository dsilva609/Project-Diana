using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.WebApi.Features.Wish.Retrieve
{
    public class WishGetByIdRequest : IRequest<WishRecord>
    {
        public ApplicationUser User { get; }
        public int WishId { get; }

        public WishGetByIdRequest(ApplicationUser user, int wishId)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));
            Guard.Against.Default(wishId, nameof(wishId));

            User = user;
            WishId = wishId;
        }
    }
}