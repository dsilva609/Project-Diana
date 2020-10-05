using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Album.AlbumById
{
    public class AlbumGetByIdRequestHandler : IRequestHandler<AlbumGetByIdRequest, AlbumRecord>
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICurrentUserService _userService;

        public AlbumGetByIdRequestHandler(IQueryDispatcher queryDispatcher, ICurrentUserService userService)
        {
            _queryDispatcher = queryDispatcher;
            _userService = userService;
        }

        public async Task<AlbumRecord> Handle(AlbumGetByIdRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<AlbumGetByIdQuery, AlbumRecord>(new AlbumGetByIdQuery(request.Id, await _userService.GetCurrentUser()));
    }
}