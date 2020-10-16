using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Album.AlbumIncrementPlayCount
{
    public class AlbumIncrementPlayCountRequest : IRequest
    {
        public int AlbumId { get; }
        public ApplicationUser User { get; }

        public AlbumIncrementPlayCountRequest(int albumId, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(albumId, nameof(albumId));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            AlbumId = albumId;
            User = user;
        }
    }
}