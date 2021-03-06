using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.Update
{
    public class WishUpdateRequestHandler : IRequestHandler<WishUpdateRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public WishUpdateRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(WishUpdateRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(
                new WishUpdateCommand(
                    request.ApiId,
                    request.Category,
                    request.ImageUrl,
                    request.ItemType,
                    request.Notes,
                    request.Owned,
                    request.Title,
                request.User.Id,
                request.WishId));
    }
}