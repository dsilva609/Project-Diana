using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumAddToShowcase
{
    public class AlbumAddToShowcaseRequestHandler : IRequestHandler<AlbumAddToShowcaseRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AlbumAddToShowcaseRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(AlbumAddToShowcaseRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new AlbumAddToShowcaseCommand(request.Id, request.User));
    }
}