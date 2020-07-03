using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Sql.Bases.Commands;

namespace Project.Diana.Data.Sql.Bases.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            Guard.Against.Null(command, nameof(command));

            var service = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;

            Guard.Against.Null(service, nameof(service));

            await service.Handle(command);
        }
    }
}