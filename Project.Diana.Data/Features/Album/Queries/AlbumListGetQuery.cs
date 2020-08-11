using System.Collections.Generic;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumListGetQuery : IQuery<IEnumerable<AlbumRecord>>
    {
        public ApplicationUser User { get; }

        public AlbumListGetQuery(ApplicationUser user) => User = user;
    }
}