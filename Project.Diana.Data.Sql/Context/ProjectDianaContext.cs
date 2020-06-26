using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public class ProjectDianaContext : DbContext
    {
        public IQueryable<WishRecord> Wishes => Set<WishRecord>().AsNoTracking();

        public ProjectDianaContext(DbContextOptions<ProjectDianaContext> options) : base(options)
        {
        }
    }
}