using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Stats;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Stats
{
    public class UserStatsGetRequestHandlerTests
    {
        private readonly AlbumStats _albumStats;
        private readonly BookStats _bookStats;
        private readonly UserStatsGetRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly UserStatsGetRequest _testRequest;

        public UserStatsGetRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();

            _testRequest = new UserStatsGetRequest(1);

            _albumStats = fixture.Create<AlbumStats>();
            _bookStats = fixture.Create<BookStats>();

            _queryDispatcher
                .Setup(x => x.Dispatch<AlbumStatsGetQuery, AlbumStats>(It.Is<AlbumStatsGetQuery>(q => q.UserId == _testRequest.UserId)))
                .ReturnsAsync(_albumStats);

            _queryDispatcher
                .Setup(x => x.Dispatch<BookStatsGetQuery, BookStats>(It.Is<BookStatsGetQuery>(q => q.UserId == _testRequest.UserId)))
                .ReturnsAsync(_bookStats);

            _handler = new UserStatsGetRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Gets_Album_Stats()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<AlbumStatsGetQuery, AlbumStats>(It.IsAny<AlbumStatsGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Gets_Book_Stats()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<BookStatsGetQuery, BookStats>(It.IsAny<BookStatsGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Gets_Stats()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
            result.TotalCount.Should().Be(_albumStats.AlbumCount + _bookStats.BookCount);
            result.CompletedCount.Should().Be(_albumStats.CompletedAlbums + _bookStats.CompletedBookCount);
            result.InProgressCount.Should().Be(_albumStats.InProgressAlbums + _bookStats.InProgressBookCount);
            result.NotCompletedCount.Should().Be(_albumStats.NotCompletedAlbums + _bookStats.NotStartedBookCount);
            result.AlbumStats.Should().NotBeNull();
            result.BookStats.Should().NotBeNull();
        }
    }
}