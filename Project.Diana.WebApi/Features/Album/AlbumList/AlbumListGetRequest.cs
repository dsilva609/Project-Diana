using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Album.AlbumList
{
    public class AlbumListGetRequest : IRequest<IEnumerable<AlbumRecord>>
    {
        [CanBeNull] public ApplicationUser User { get; }

        public AlbumListGetRequest(ApplicationUser user) => User = user;
    }
}