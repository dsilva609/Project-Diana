using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Queries
{
    public class AlbumGetByIdQueryHandler : IQueryHandler<AlbumGetByIdQuery, AlbumRecord>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public AlbumGetByIdQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<AlbumRecord> Handle(AlbumGetByIdQuery query)
            => string.IsNullOrWhiteSpace(query.User?.Id)
                ? await _context.Albums.FirstOrDefaultAsync(album => album.Id == query.Id)
                : await _context.Albums.FirstOrDefaultAsync(album => album.Id == query.Id && album.UserId == query.User.Id);
    }
}