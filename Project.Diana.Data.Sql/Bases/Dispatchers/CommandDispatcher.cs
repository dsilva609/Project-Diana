using System;
using System.Threading.Tasks;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Sql.Bases.Commands;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task Dispatch<T>(T command) where T : ICommand
        {
            //--TODO: needs validation
            var service = _serviceProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;

            await service.Handle(command);
        }
    }
}