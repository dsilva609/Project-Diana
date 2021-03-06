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
    public class WishGetListByUserIdQueryHandler : IQueryHandler<WishGetListByUserIdQuery, IEnumerable<WishRecord>>
    {
        private readonly IProjectDianaReadonlyContext _projectDianaContext;

        public WishGetListByUserIdQueryHandler(IProjectDianaReadonlyContext projectDianaContext) => _projectDianaContext = projectDianaContext;

        public async Task<IEnumerable<WishRecord>> Handle(WishGetListByUserIdQuery query) =>
            await _projectDianaContext.Wishes.Where(w => w.UserId == query.UserId).ToListAsync();
    }
}