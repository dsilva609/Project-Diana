using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<Unit> Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    }
}