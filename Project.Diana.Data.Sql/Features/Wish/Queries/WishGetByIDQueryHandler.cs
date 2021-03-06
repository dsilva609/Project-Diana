using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Queries
{
    public class WishGetByIdQueryHandler : IQueryHandler<WishGetByIdQuery, WishRecord>
    {
        private readonly IProjectDianaReadonlyContext _projectDianaContext;

        public WishGetByIdQueryHandler(IProjectDianaReadonlyContext projectDianaContext) => _projectDianaContext = projectDianaContext;

        public async Task<WishRecord> Handle(WishGetByIdQuery query)
            => await _projectDianaContext.Wishes.FirstOrDefaultAsync(wish =>
                wish.UserId == query.UserId
                && wish.Id == query.WishId);
    }
}