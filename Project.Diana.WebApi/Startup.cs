using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Project.Diana.WebApi
{
    public class Startup
    {
        private readonly Container _container = new Container();
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               })
               .UseSimpleInjector(_container);

            _container.Verify();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddMediatR(typeof(Startup))
                .AddSimpleInjector(_container);

            //--TODO: pull out individual registrations
            InitializeContainer();
        }

        private void InitializeContainer()
        {
        }
    }
}