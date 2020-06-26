using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Kledex;
using Moq;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.WebApi.Features.Wish;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish
{
    public class WishGetByIDRequestHandlerTests
    {
        private readonly Mock<IDispatcher> _dispatcher;
        private readonly WishGetByIdRequestHandler _handler;
        private readonly WishGetByIDRequest _testRequest;

        public WishGetByIDRequestHandlerTests()
        {
            var fixture = new Fixture();

            _dispatcher = new Mock<IDispatcher>();
            _testRequest = fixture.Create<WishGetByIDRequest>();

            _dispatcher
                .Setup(x => x.GetResultAsync(It.Is<WishGetByIDQuery>(q => q != null)))
                .Returns(Task.FromResult(fixture.Create<string>()));

            _handler = new WishGetByIdRequestHandler(_dispatcher.Object);
        }

        [Fact]
        public async Task HandlerCallsDispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _dispatcher.Verify(x => x.GetResultAsync(It.Is<WishGetByIDQuery>(q => q != null)), Times.Once);
        }

        [Fact]
        public async Task HandlerReturnsWish()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNullOrWhiteSpace();
        }
    }
}