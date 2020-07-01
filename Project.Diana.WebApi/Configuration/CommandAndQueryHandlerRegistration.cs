using Microsoft.Extensions.DependencyInjection;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Features.Wish.Queries;

namespace Project.Diana.WebApi.Configuration
{
    public static class CommandAndQueryHandlerRegistration
    {
        public static IServiceCollection RegisterCommandAndQueryHandlers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<WishGetByIDQueryHandler>()
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            return services;
        }
    }
}