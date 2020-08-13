using System.Collections.Generic;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Album.AlbumList
{
    public class AlbumListGetRequest : IRequest<IEnumerable<AlbumRecord>>
    {
        public int ItemCount { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public AlbumListGetRequest(int itemCount, ApplicationUser user)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount;
            User = user;
        }
    }
}