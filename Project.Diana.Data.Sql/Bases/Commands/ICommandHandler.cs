using System.Threading.Tasks;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Sql.Bases.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}