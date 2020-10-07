using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumList
{
    public class AlbumListGetRequestHandlerTests
    {
        private readonly AlbumListGetRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly AlbumListGetRequest _testRequest;

        public AlbumListGetRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<AlbumListGetRequest>();

            _queryDispatcher
                .Setup(x => x.Dispatch<AlbumListGetQuery, AlbumListResponse>(It.IsNotNull<AlbumListGetQuery>()))
                .ReturnsAsync(fixture.Create<AlbumListResponse>());

            _handler = new AlbumListGetRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_AlbumListGetQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<AlbumListGetQuery, AlbumListResponse>(It.IsNotNull<AlbumListGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Albums.Should().NotBeNullOrEmpty();
        }
    }
}