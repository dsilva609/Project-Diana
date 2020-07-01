using System.Threading.Tasks;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}