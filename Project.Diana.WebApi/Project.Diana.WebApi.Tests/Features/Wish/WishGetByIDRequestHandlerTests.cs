using System.Threading;
using AutoFixture;
using FluentAssertions;
using Project.Diana.WebApi.Features.Wish;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish
{
    public class WishGetByIDRequestHandlerTests
    {
        private readonly WishGetByIdRequestHandler _handler;
        private readonly WishGetByIDRequest _testRequest;

        public WishGetByIDRequestHandlerTests()
        {
            var fixture = new Fixture();

            _testRequest = fixture.Create<WishGetByIDRequest>();

            _handler = new WishGetByIdRequestHandler();
        }

        [Fact]
        public void HandlerReturnsWish()
        {
            var result = _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}