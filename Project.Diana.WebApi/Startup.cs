using Kledex.Extensions;
using Kledex.Queries;
using Kledex.Validation.FluentValidation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Queries;

namespace Project.Diana.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseKledex();
            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddDbContext<ProjectDianaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<IQueryHandler<WishGetByIDQuery, string>, WishGetByIDQueryHandler>();
            services.AddKledex(typeof(WishGetByIDQuery), typeof(WishGetByIDQueryHandler)).AddFluentValidation();
        }
    }
}