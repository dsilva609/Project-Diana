using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Diana.Data.Features.Album;
using Project.Diana.WebApi.Configuration;
using Project.Diana.WebApi.Configuration.Providers;

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

            app.UseCors("CorsPolicy")
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks(string.Empty);
                });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            => services
                .AddAutoMapper(typeof(AlbumMappingProfile))
                .AddDiscogsProvider(Configuration)
                .AddGoogleBooksProvider()
                .AddHttpContextAccessor()
                .AddMediatR(typeof(Startup).Assembly)
                .RegisterAuthorization(Configuration)
                .RegisterCoreServices()
                .RegisterCors()
                .RegisterCommandAndQueryHandlers()
                .RegisterDBContext(Configuration)
                .RegisterHelperServices()
                .RegisterSettings(Configuration);
    }
}