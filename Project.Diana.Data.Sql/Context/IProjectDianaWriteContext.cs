using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaWriteContext
    {
        public DbSet<WishRecord> Wishes { get; set; }
    }
}