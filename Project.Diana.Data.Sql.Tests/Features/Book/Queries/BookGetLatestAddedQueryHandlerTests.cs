using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Queries
{
    public class BookGetLatestAddedQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly BookGetLatestAddedQueryHandler _handler;
        private readonly BookGetLatestAddedQuery _testQuery;

        public BookGetLatestAddedQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = new BookGetLatestAddedQuery(5);

            _handler = new BookGetLatestAddedQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Book_List()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Books.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Requested_Count()
        {
            await InitializeRecords();

            var query = new BookGetLatestAddedQuery(1);

            var result = await _handler.Handle(query);

            result.Books.Should().HaveCount(query.ItemCount);
        }

        [Fact]
        public async Task Handler_Sorts_By_Date_Added()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            var newestBook = result.Books.FirstOrDefault();
            var previousBook = result.Books.ElementAtOrDefault(1);

            newestBook.DateAdded.Should().BeAfter(previousBook.DateAdded);
        }

        private async Task InitializeRecords()
        {
            await _context.BookRecords.AddRangeAsync(_fixture.CreateMany<BookRecord>());

            await _context.SaveChangesAsync();
        }
    }
}