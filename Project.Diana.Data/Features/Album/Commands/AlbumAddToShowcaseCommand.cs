using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Commands
{
    public class AlbumAddToShowcaseCommand : ICommand
    {
        public int AlbumId { get; }
        public ApplicationUser User { get; }

        public AlbumAddToShowcaseCommand(int albumId, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(albumId, nameof(albumId));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            AlbumId = albumId;
            User = user;
        }
    }
}