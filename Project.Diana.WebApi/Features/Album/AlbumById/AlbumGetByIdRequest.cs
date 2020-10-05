using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Album.AlbumById
{
    public class AlbumGetByIdRequest : IRequest<AlbumRecord>
    {
        public int Id { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public AlbumGetByIdRequest(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Id = id;
            User = user;
        }
    }
}