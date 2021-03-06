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
            var albumQuery = _context.Albums;

            if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            {
                albumQuery = albumQuery.Where(q
                      => q.Artist.ToLower().Contains(query.SearchQuery.ToLower())
                         || q.Title.ToLower().Contains(query.SearchQuery.ToLower()));
            }

            var totalCount = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await albumQuery.CountAsync()
                : await albumQuery.CountAsync(album => album.UserId == query.User.Id);

            albumQuery = albumQuery.OrderBy(album => album.Artist).ThenBy(album => album.Title).Skip(query.ItemCount * query.Page);

            var albums = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await albumQuery.Take(query.ItemCount).ToListAsync()
                : await albumQuery.Where(a => a.UserId == query.User.Id).Take(query.ItemCount).ToListAsync();

            return new AlbumListResponse
            {
                Albums = albums,
                TotalCount = totalCount
            };
        }
    }
}