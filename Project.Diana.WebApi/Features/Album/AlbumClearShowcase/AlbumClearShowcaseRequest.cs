using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Album.AlbumClearShowcase
{
    public class AlbumClearShowcaseRequest : IRequest
    {
        public ApplicationUser User { get; }

        public AlbumClearShowcaseRequest(ApplicationUser user)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            User = user;
        }
    }
}