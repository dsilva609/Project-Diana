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
    public class GlobalStatsGetRequestHandler : IRequestHandler<GlobalStatsGetRequest, StatsResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GlobalStatsGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<StatsResponse> Handle(GlobalStatsGetRequest request, CancellationToken cancellationToken)
        {
            var albumStats = await _queryDispatcher.Dispatch<AlbumStatsGetQuery, AlbumStats>(new AlbumStatsGetQuery());

            var bookStats = await _queryDispatcher.Dispatch<BookStatsGetQuery, BookStats>(new BookStatsGetQuery());

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