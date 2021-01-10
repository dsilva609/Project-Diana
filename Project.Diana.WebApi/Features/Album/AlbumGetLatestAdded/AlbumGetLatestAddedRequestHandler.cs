using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumGetLatestAdded
{
    public class AlbumGetLatestAddedRequestHandler : IRequestHandler<AlbumGetLatestAddedRequest, AlbumListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public AlbumGetLatestAddedRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public Task<AlbumListResponse> Handle(AlbumGetLatestAddedRequest request, CancellationToken cancellationToken)
            => _queryDispatcher.Dispatch<AlbumGetLatestAddedQuery, AlbumListResponse>(new AlbumGetLatestAddedQuery(request.ItemCount));
    }
}