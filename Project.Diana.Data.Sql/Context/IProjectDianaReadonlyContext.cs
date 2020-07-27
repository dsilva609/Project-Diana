using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaReadonlyContext
    {
        IQueryable<WishRecord> Wishes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}