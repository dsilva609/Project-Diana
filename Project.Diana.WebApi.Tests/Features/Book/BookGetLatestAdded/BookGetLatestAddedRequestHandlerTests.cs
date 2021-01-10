using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookGetLatestAdded;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookGetLatestAdded
{
    public class BookGetLatestAddedRequestHandlerTests
    {
        private readonly BookGetLatestAddedRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly BookGetLatestAddedRequest _testRequest;

        public BookGetLatestAddedRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<BookGetLatestAddedRequest>();

            _queryDispatcher
                .Setup(x => x.Dispatch<BookGetLatestAddedQuery, BookListResponse>(It.IsNotNull<BookGetLatestAddedQuery>()))
                .ReturnsAsync(fixture.Create<BookListResponse>());

            _handler = new BookGetLatestAddedRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_BookGetLatestAddedQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<BookGetLatestAddedQuery, BookListResponse>(It.IsNotNull<BookGetLatestAddedQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Books.Should().NotBeNullOrEmpty();
        }
    }
}