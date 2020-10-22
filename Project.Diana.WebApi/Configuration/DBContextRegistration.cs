using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.WebApi.Configuration
{
    public static class DBContextRegistration
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<IProjectDianaReadonlyContext, ProjectDianaReadonlyContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<ProjectDianaReadonlyContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<IProjectDianaWriteContext, ProjectDianaWriteContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}