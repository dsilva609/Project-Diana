using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Sql.Bases.Queries;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            Guard.Against.Null(query, nameof(query));

            var service = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;

            Guard.Against.Null(service, nameof(service));

            return await service.Handle(query);
        }
    }
}