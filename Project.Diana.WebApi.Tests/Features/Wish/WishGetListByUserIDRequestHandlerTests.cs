using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish
{
    public class WishGetListByUserIDRequestHandlerTests
    {
        private readonly WishGetListByUserIDRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly WishGetListByUserIDRequest _testRequest;

        public WishGetListByUserIDRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<WishGetListByUserIDRequest>();

            _queryDispatcher
                .Setup(x =>
                    x.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(
                        It.IsNotNull<WishGetListByUserIDQuery>()))
                .ReturnsAsync(fixture.Create<IEnumerable<WishRecord>>());

            _handler = new WishGetListByUserIDRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_QueryDispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(It.IsNotNull<WishGetListByUserIDQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNullOrEmpty();
        }
    }
}