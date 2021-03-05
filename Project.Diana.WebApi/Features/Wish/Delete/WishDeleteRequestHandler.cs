using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.Delete
{
    public class WishDeleteRequestHandler : IRequestHandler<WishDeleteRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public WishDeleteRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(WishDeleteRequest request, CancellationToken cancellationToken)
             => await _commandDispatcher.Dispatch(new WishDeleteCommand(request.WishId, request.User));
    }
}