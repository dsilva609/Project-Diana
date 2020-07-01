using System.Threading.Tasks;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Queries;

namespace Project.Diana.Data.Sql.Features.Wish.Queries
{
    public class WishGetByIDQueryHandler : IQueryHandler<WishGetByIDQuery, string>
    {
        public Task<string> Handle(WishGetByIDQuery query) => throw new System.NotImplementedException();
    }
}