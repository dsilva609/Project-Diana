using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Queries
{
    public class AlbumListGetQueryHandler : IQueryHandler<AlbumListGetQuery, AlbumListResponse>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public AlbumListGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<AlbumListResponse> Handle(AlbumListGetQuery query)
        {
            var totalCount = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await _context.Albums.CountAsync()
                : await _context.Albums.CountAsync(album => album.UserID == query.User.Id);

            var albumQuery = _context.Albums.OrderBy(album => album.Artist).ThenBy(album => album.Title).Skip(query.ItemCount * query.Page);

            var albums = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await albumQuery.Take(query.ItemCount).ToListAsync()
                : await albumQuery.Where(a => a.UserID == query.User.Id).Take(query.ItemCount).ToListAsync();

            return new AlbumListResponse
            {
                Albums = albums,
                TotalCount = totalCount
            };
        }
    }
}