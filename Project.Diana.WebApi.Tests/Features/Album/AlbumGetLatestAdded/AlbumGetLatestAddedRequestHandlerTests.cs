using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumGetLatestAdded;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumGetLatestAdded
{
    public class AlbumGetLatestAddedRequestHandlerTests
    {
        private readonly AlbumGetLatestAddedRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly AlbumGetLatestAddedRequest _testRequest;

        public AlbumGetLatestAddedRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<AlbumGetLatestAddedRequest>();

            _queryDispatcher
                .Setup(x => x.Dispatch<AlbumGetLatestAddedQuery, AlbumListResponse>(It.IsNotNull<AlbumGetLatestAddedQuery>()))
                .ReturnsAsync(fixture.Create<AlbumListResponse>());

            _handler = new AlbumGetLatestAddedRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_AlbumGetLatestAddedQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<AlbumGetLatestAddedQuery, AlbumListResponse>(It.IsNotNull<AlbumGetLatestAddedQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Albums.Should().NotBeNullOrEmpty();
        }
    }
}