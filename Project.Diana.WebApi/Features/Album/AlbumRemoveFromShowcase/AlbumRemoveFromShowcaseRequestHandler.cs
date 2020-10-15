using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumRemoveFromShowcase
{
    public class AlbumRemoveFromShowcaseRequestHandler : IRequestHandler<AlbumRemoveFromShowcaseRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AlbumRemoveFromShowcaseRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(AlbumRemoveFromShowcaseRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new AlbumRemoveFromShowcaseCommand(request.Id, request.User));
    }
}