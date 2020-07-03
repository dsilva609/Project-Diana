using System.Threading.Tasks;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Sql.Bases.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}