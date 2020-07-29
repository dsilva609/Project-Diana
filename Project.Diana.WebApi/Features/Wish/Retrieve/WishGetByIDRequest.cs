using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.WebApi.Features.Wish.Retrieve
{
    public class WishGetByIDRequest : IRequest<WishRecord>
    {
        public ApplicationUser User { get; }
        public int WishID { get; }

        public WishGetByIDRequest(ApplicationUser user, int wishID)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));
            Guard.Against.Default(wishID, nameof(wishID));

            User = user;
            WishID = wishID;
        }
    }
}