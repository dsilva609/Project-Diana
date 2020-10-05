using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumGetByIdQuery : IQuery<AlbumRecord>
    {
        public int Id { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public AlbumGetByIdQuery(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Id = id;
            User = user;
        }
    }
}