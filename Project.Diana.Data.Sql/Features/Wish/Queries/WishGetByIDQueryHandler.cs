using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Queries
{
    public class WishGetByIDQueryHandler : IQueryHandler<WishGetByIDQuery, WishRecord>
    {
        private readonly IProjectDianaContext _projectDianaContext;

        public WishGetByIDQueryHandler(IProjectDianaContext projectDianaContext) => _projectDianaContext = projectDianaContext;

        public async Task<WishRecord> Handle(WishGetByIDQuery query)
            => await _projectDianaContext.Wishes.FirstOrDefaultAsync(wish =>
                wish.UserID == query.UserID
                && wish.ID == query.WishId);
    }
}