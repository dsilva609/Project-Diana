using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public class ProjectDianaContext : DbContext, IProjectDianaContext
    {
        public IQueryable<WishRecord> Wishes => WishRecords.AsNoTracking();
        public DbSet<WishRecord> WishRecords { get; set; }

        public ProjectDianaContext(DbContextOptions<ProjectDianaContext> options) : base(options)
        {
        }
    }
}