using Kledex.Queries;
using Project.Diana.Data.Features.Wish.Queries;

namespace Project.Diana.Data.Sql.Features.Wish.Queries
{
    public class WishGetByIDQueryHandler : IQueryHandler<WishGetByIDQuery, string>
    {
        public string Handle(WishGetByIDQuery query) => throw new System.NotImplementedException();
    }
}