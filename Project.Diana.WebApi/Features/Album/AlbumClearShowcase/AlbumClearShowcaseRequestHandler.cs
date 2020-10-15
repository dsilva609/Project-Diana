using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumClearShowcase
{
    public class AlbumClearShowcaseRequestHandler : IRequestHandler<AlbumClearShowcaseRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AlbumClearShowcaseRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(AlbumClearShowcaseRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new AlbumClearShowcaseCommand(request.User));
    }
}