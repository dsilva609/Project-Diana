﻿using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumById;
using Project.Diana.WebApi.Helpers;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumById
{
    public class AlbumGetByIdRequestHandlerTests
    {
        private readonly AlbumGetByIdRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly AlbumGetByIdRequest _testRequest;
        private readonly Mock<ICurrentUserService> _userService;

        public AlbumGetByIdRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<AlbumGetByIdRequest>();
            _userService = new Mock<ICurrentUserService>();

            _queryDispatcher.Setup(x => x.Dispatch<AlbumGetByIdQuery, AlbumRecord>(It.IsNotNull<AlbumGetByIdQuery>())).ReturnsAsync(fixture.Create<AlbumRecord>());
            _userService.Setup(x => x.GetCurrentUser()).ReturnsAsync(fixture.Create<ApplicationUser>());

            _handler = new AlbumGetByIdRequestHandler(_queryDispatcher.Object, _userService.Object);
        }

        [Fact]
        public async Task Handler_Calls_Current_User()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _userService.Verify(x => x.GetCurrentUser(), Times.Once);
        }

        [Fact]
        public async Task Handler_Calls_Query_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<AlbumGetByIdQuery, AlbumRecord>(It.IsNotNull<AlbumGetByIdQuery>()), Times.Once());
        }

        [Fact]
        public async Task Handler_Returns_Album()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}