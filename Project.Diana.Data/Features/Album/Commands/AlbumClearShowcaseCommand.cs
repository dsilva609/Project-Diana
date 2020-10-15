using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Commands
{
    public class AlbumClearShowcaseCommand : ICommand
    {
        public ApplicationUser User { get; }

        public AlbumClearShowcaseCommand(ApplicationUser user)
        {
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            User = user;
        }
    }
}