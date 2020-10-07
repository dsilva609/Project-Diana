using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookList
{
    public class BookListGetRequestHandlerTests
    {
        private readonly BookListGetRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly BookListGetRequest _testRequest;

        public BookListGetRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<BookListGetRequest>();

            _queryDispatcher
                .Setup(x => x.Dispatch<BookListGetQuery, BookListResponse>(It.IsNotNull<BookListGetQuery>()))
                .ReturnsAsync(fixture.Create<BookListResponse>());

            _handler = new BookListGetRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_BookListGetQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<BookListGetQuery, BookListResponse>(It.IsNotNull<BookListGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Books.Should().NotBeNullOrEmpty();
        }
    }
}