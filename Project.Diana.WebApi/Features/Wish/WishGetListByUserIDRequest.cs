using System.Collections.Generic;
using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetListByUserIDRequest : IRequest<IEnumerable<WishRecord>>
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