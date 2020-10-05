using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumList
{
    public class AlbumListGetRequestHandler : IRequestHandler<AlbumListGetRequest, IEnumerable<AlbumRecord>>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public AlbumListGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<IEnumerable<AlbumRecord>> Handle(AlbumListGetRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<AlbumListGetQuery, IEnumerable<AlbumRecord>>(new AlbumListGetQuery(request.ItemCount, request.User));
    }
}