using System;
using System.Threading.Tasks;
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
            //--TODO: needs validation
            var service = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;

            return await service.Handle(query);
        }
    }
}