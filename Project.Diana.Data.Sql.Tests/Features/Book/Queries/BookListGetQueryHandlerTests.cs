using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Queries
{
    public class BookListGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly IEnumerable<BookRecord> _bookRecords;
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly BookListGetQueryHandler _handler;
        private readonly BookListGetQuery _testQuery;
        private readonly BookRecord _testRecord;

        public BookListGetQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _bookRecords = _fixture.Create<IEnumerable<BookRecord>>();
            _testQuery = new BookListGetQuery(69, _fixture.Create<ApplicationUser>());
            _testRecord = _fixture
                .Build<BookRecord>()
                .With(book => book.UserID, _testQuery.User.Id)
                .Create();

            _handler = new BookListGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Book_For_UserID()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
            result.All(b => b.UserID == _testQuery.User.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Books()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Records_When_There_Is_No_UserID()
        {
            _testQuery.User.Id = string.Empty;

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
            result.Should().Contain(x => x.UserID != _testQuery.User.Id);
        }

        [Fact]
        public async Task Handler_Returns_Requested_Count()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var request = new BookListGetQuery(1, _testQuery.User);

            var result = await _handler.Handle(request);

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handler_Sorts_By_Author()
        {
            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Bendis")
                .With(book => book.UserID, _testQuery.User.Id)
                .Create();

            var expectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Archibald")
                .With(book => book.UserID, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(unexpectedBook);
            await _context.BookRecords.AddAsync(expectedBook);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.First().Author.Should().Be(expectedBook.Author);
        }

        [Fact]
        public async Task Handler_Sorts_By_Author_Then_Title()
        {
            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Wonder Woman")
                .With(book => book.UserID, _testQuery.User.Id)
                .Create();

            var expectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Old Guard")
                .With(book => book.UserID, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(unexpectedBook);
            await _context.BookRecords.AddAsync(expectedBook);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.First().Author.Should().Be(expectedBook.Author);
        }

        private async Task InitializeRecords()
        {
            await _context.BookRecords.AddRangeAsync(_bookRecords);

            await _context.SaveChangesAsync();
        }
    }
}