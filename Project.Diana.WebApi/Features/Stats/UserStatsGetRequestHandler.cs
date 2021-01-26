using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Stats
{
    public class UserStatsGetRequestHandler : IRequestHandler<UserStatsGetRequest, StatsResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public UserStatsGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<StatsResponse> Handle(UserStatsGetRequest request, CancellationToken cancellationToken)
        {
            var albumStats = await _queryDispatcher.Dispatch<AlbumStatsGetQuery, AlbumStats>(new AlbumStatsGetQuery(request.UserId));

            var bookStats = await _queryDispatcher.Dispatch<BookStatsGetQuery, BookStats>(new BookStatsGetQuery(request.UserId));

            return new StatsResponse
            {
                AlbumStats = albumStats,
                BookStats = bookStats,
                CompletedCount = albumStats.CompletedAlbums + bookStats.CompletedBookCount,
                InProgressCount = albumStats.InProgressAlbums + bookStats.InProgressBookCount,
                NotCompletedCount = albumStats.NotCompletedAlbums + bookStats.NotStartedBookCount,
                TotalCount = albumStats.AlbumCount + bookStats.BookCount
            };
        }
    }
}