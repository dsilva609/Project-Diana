using System.Threading.Tasks;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task Dispatch<T>(T command) where T : ICommand;
    }
}