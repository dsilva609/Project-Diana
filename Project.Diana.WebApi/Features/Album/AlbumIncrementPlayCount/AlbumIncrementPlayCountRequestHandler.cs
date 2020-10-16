using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumIncrementPlayCount
{
    public class AlbumIncrementPlayCountRequestHandler : IRequestHandler<AlbumIncrementPlayCountRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AlbumIncrementPlayCountRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(AlbumIncrementPlayCountRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(new AlbumIncrementPlayCountCommand(request.AlbumId, request.User));
    }
}