using System.Linq;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaContext
    {
        IQueryable<WishRecord> Wishes { get; }
    }
}