using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumList
{
    public class AlbumListGetRequestHandler : IRequestHandler<AlbumListGetRequest, AlbumListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public AlbumListGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<AlbumListResponse> Handle(AlbumListGetRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<AlbumListGetQuery, AlbumListResponse>(new AlbumListGetQuery(request.ItemCount, request.Page, request.SearchQuery, request.User));
    }
}