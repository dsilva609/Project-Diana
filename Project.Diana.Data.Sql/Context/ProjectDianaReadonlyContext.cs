using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Sql.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public class ProjectDianaReadonlyContext : IdentityDbContext<ApplicationUser>, IProjectDianaReadonlyContext
    {
        public DbSet<AlbumRecord> AlbumRecords { get; set; }
        public IQueryable<AlbumRecord> Albums => AlbumRecords.AsNoTracking();
        public DbSet<BookRecord> BookRecords { get; set; }
        public IQueryable<BookRecord> Books => BookRecords.AsNoTracking();
        public IQueryable<WishRecord> Wishes => WishRecords.AsNoTracking();
        public DbSet<WishRecord> WishRecords { get; set; }

        public ProjectDianaReadonlyContext(DbContextOptions<ProjectDianaReadonlyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WishRecordConfiguration).Assembly);
        }
    }
}