using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.Data.Features.Settings;
using RestSharp;

namespace Project.Diana.WebApi.Configuration
{
    public static class CoreRegistration
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddScoped<IRestClient, RestClient>();

            services
                .AddMvc()
                .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<GlobalSettings>();
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    }
                );

            services.AddControllers();

            return services;
        }
    }
}