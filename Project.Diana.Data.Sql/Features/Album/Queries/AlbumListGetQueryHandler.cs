using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Queries
{
    public class AlbumListGetQueryHandler : IQueryHandler<AlbumListGetQuery, IEnumerable<AlbumRecord>>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public AlbumListGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<IEnumerable<AlbumRecord>> Handle(AlbumListGetQuery query)
        {
            var albumQuery = _context.Albums;

            if (string.IsNullOrWhiteSpace(query.User?.Id))
            {
                return await albumQuery.ToListAsync();
            }

            return await albumQuery.Where(a => a.UserID == query.User.Id).ToListAsync();
        }
    }
}