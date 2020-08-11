using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Project.Diana.WebApi.Helpers;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumList
{
    public class AlbumListGetRequestHandlerTests
    {
        private readonly AlbumListGetRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly AlbumListGetRequest _testRequest;
        private readonly Mock<ICurrentUserService> _userService;

        public AlbumListGetRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<AlbumListGetRequest>();
            _userService = new Mock<ICurrentUserService>();

            _queryDispatcher
                .Setup(x => x.Dispatch<AlbumListGetQuery, IEnumerable<AlbumRecord>>(It.IsNotNull<AlbumListGetQuery>()))
                .ReturnsAsync(fixture.Create<IEnumerable<AlbumRecord>>());

            _handler = new AlbumListGetRequestHandler(_userService.Object, _queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_AlbumListGetQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<AlbumListGetQuery, IEnumerable<AlbumRecord>>(It.IsNotNull<AlbumListGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Calls_UserService()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _userService.Verify(x => x.GetCurrentUser(), Times.Once());
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNullOrEmpty();
        }
    }
}