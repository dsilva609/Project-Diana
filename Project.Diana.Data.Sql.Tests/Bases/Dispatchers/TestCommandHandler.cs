using System.Threading.Tasks;
using Project.Diana.Data.Sql.Bases.Commands;

namespace Project.Diana.Data.Sql.Tests.Bases.Dispatchers
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task Handle(TestCommand command) => Task.CompletedTask;
    }
}