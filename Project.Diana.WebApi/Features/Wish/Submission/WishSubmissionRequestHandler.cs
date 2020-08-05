using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.Submission
{
    public class WishSubmissionRequestHandler : IRequestHandler<WishSubmissionRequest, Unit>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public WishSubmissionRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(WishSubmissionRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new WishCreateCommand(
                request.ApiID,
                request.Category,
                request.ImageUrl,
                request.ItemType,
                request.Notes,
                request.Owned,
                request.Title,
                request.User.Id));
    }
}