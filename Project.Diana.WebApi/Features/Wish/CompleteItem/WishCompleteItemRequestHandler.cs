using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.CompleteItem
{
    public class WishCompleteItemRequestHandler : IRequestHandler<WishCompleteItemRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public WishCompleteItemRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(WishCompleteItemRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new WishCompleteItemCommand(request.User.Id, request.WishID));
    }
}