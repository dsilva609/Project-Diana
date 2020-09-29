using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookList;
using Project.Diana.WebApi.Helpers;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookList
{
    public class BookListGetRequestHandlerTests
    {
        private readonly BookListGetRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly BookListGetRequest _testRequest;
        private readonly Mock<ICurrentUserService> _userService;

        public BookListGetRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<BookListGetRequest>();
            _userService = new Mock<ICurrentUserService>();

            _queryDispatcher
                .Setup(x => x.Dispatch<BookListGetQuery, IEnumerable<BookRecord>>(It.IsNotNull<BookListGetQuery>()))
                .ReturnsAsync(fixture.Create<IEnumerable<BookRecord>>());

            _handler = new BookListGetRequestHandler(_queryDispatcher.Object, _userService.Object);
        }

        [Fact]
        public async Task Handler_Calls_BookListGetQuery()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<BookListGetQuery, IEnumerable<BookRecord>>(It.IsNotNull<BookListGetQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Calls_UserService()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _userService.Verify(x => x.GetCurrentUser(), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().NotBeNullOrEmpty();
        }
    }
}