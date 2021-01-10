using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Queries
{
    public class AlbumGetLatestAddedQueryHandler : IQueryHandler<AlbumGetLatestAddedQuery, AlbumListResponse>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public AlbumGetLatestAddedQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<AlbumListResponse> Handle(AlbumGetLatestAddedQuery query)
        {
            var albums = await _context.Albums
                .OrderByDescending(album => album.DateAdded)
                .Take(query.ItemCount)
                .ToListAsync();

            return new AlbumListResponse { Albums = albums, TotalCount = albums.Count };
        }
    }
}