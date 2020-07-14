using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Queries
{
    public class WishGetListByUserIDQueryHandler : IQueryHandler<WishGetListByUserIDQuery, IEnumerable<WishRecord>>
    {
        private readonly IProjectDianaContext _projectDianaContext;

        public WishGetListByUserIDQueryHandler(IProjectDianaContext projectDianaContext) => _projectDianaContext = projectDianaContext;

        public async Task<IEnumerable<WishRecord>> Handle(WishGetListByUserIDQuery query) =>
            await _projectDianaContext.Wishes.Where(w => w.UserID == query.UserID).ToListAsync();
    }
}