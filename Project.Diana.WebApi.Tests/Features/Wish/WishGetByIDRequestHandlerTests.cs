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
    public class WishGetByIDRequestHandlerTests
    {
        private readonly WishGetByIdRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly WishGetByIDRequest _testRequest;

        public WishGetByIDRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<WishGetByIDRequest>();

            _queryDispatcher.Setup(x => x.Dispatch<WishGetByIDQuery, WishRecord>(It.Is<WishGetByIDQuery>(y => y != null))).ReturnsAsync(new WishRecord());

            _handler = new WishGetByIdRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task HandlerCallsDispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<WishGetByIDQuery, WishRecord>(It.Is<WishGetByIDQuery>(q => q != null)), Times.Once);
        }

        [Fact]
        public async Task HandlerReturnsWish()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}