using System.Threading.Tasks;
using Project.Diana.Data.Sql.Bases.Queries;

namespace Project.Diana.Data.Sql.Tests.Bases.Dispatchers
{
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<string> Handle(TestQuery query) => Task.FromResult(string.Empty);
    }
}