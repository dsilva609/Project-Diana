using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaWriteContext
    {
        DbSet<WishRecord> Wishes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}