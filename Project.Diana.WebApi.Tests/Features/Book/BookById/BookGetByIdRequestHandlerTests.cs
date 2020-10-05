using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookById
{
    public class BookGetByIdRequestHandlerTests
    {
        private readonly BookGetByIdRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly BookGetByIdRequest _testRequest;

        public BookGetByIdRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<BookGetByIdRequest>();

            _queryDispatcher.Setup(x => x.Dispatch<BookGetByIdQuery, BookRecord>(It.IsNotNull<BookGetByIdQuery>())).ReturnsAsync(fixture.Create<BookRecord>());

            _handler = new BookGetByIdRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Query_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<BookGetByIdQuery, BookRecord>(It.IsNotNull<BookGetByIdQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_Book()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}