using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Wish.CompleteItem
{
    public class WishCompleteItemRequest : IRequest
    {
        public ApplicationUser User { get; }
        public int WishID { get; }

        public WishCompleteItemRequest(ApplicationUser user, int wishID)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));
            Guard.Against.Default(wishID, nameof(wishID));

            User = user;
            WishID = wishID;
        }
    }
}