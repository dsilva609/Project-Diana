using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Sql.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public class ProjectDianaContext : IdentityDbContext<ApplicationUser>, IProjectDianaContext
    {
        public IQueryable<WishRecord> Wishes => WishRecords.AsNoTracking();
        public DbSet<WishRecord> WishRecords { get; set; }

        public ProjectDianaContext(DbContextOptions<ProjectDianaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WishRecordConfiguration).Assembly);
        }
    }
}