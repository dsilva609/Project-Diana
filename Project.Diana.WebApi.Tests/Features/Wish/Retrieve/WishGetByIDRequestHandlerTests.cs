using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish.Retrieve;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Retrieve
{
    public class WishGetByIDRequestHandlerTests
    {
        private readonly WishGetByIDRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly WishGetByIDRequest _testRequest;

        public WishGetByIDRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<WishGetByIDRequest>();

            _queryDispatcher.Setup(x => x.Dispatch<WishGetByIDQuery, WishRecord>(It.Is<WishGetByIDQuery>(y => y != null))).ReturnsAsync(new WishRecord());

            _handler = new WishGetByIDRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<WishGetByIDQuery, WishRecord>(It.Is<WishGetByIDQuery>(q => q != null)), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_Wish()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}