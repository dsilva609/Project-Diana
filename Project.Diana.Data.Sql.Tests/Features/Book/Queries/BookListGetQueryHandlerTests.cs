using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _bookRecords = _fixture.Create<IEnumerable<BookRecord>>();
            _testQuery = new BookListGetQuery(69, 0, string.Empty, _fixture.Create<ApplicationUser>());
            _testRecord = _fixture
                .Build<BookRecord>()
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            _handler = new BookListGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Gets_Next_Page_Of_Books()
        {
            var expectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Wonder Woman")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Old Guard")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(unexpectedBook);
            await _context.BookRecords.AddAsync(expectedBook);
            await _context.SaveChangesAsync();

            var query = new BookListGetQuery(1, 1, _testQuery.SearchQuery, _testQuery.User);

            var result = await _handler.Handle(query);

            result.Books.First().Title.Should().Be(expectedBook.Title);
        }

        [Fact]
        public async Task Handler_Returns_Book_For_UserId()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Books.Should().NotBeNullOrEmpty();
            result.Books.All(b => b.UserId == _testQuery.User.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Books()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Books.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Record_With_Query_For_Author()
        {
            var record = _fixture
                .Build<BookRecord>()
                .With(b => b.Author, "Rucka")
                .Create();

            await _context.BookRecords.AddAsync(record);

            await InitializeRecords();

            var request = new BookListGetQuery(1, 0, record.Author, null);

            var result = await _handler.Handle(request);

            result.Books.Should().Contain(b => b.Author == record.Author);
            result.TotalCount.Should().Be(request.ItemCount);
        }

        [Fact]
        public async Task Handler_Returns_Record_With_Query_For_Title()
        {
            var record = _fixture
                .Build<BookRecord>()
                .With(b => b.Title, "Wonder Woman")
                .Create();

            await _context.BookRecords.AddAsync(record);

            await InitializeRecords();

            var request = new BookListGetQuery(1, 0, record.Title, null);

            var result = await _handler.Handle(request);

            result.Books.Should().Contain(b => b.Title == record.Title);
            result.TotalCount.Should().Be(request.ItemCount);
        }

        [Fact]
        public async Task Handler_Returns_Records_For_Search_Query_And_User_Id()
        {
            var records = _fixture
                .Build<BookRecord>()
                .With(b => b.Title, _testQuery.SearchQuery)
                .With(b => b.UserId, _testQuery.User.Id)
                .CreateMany();

            await _context.BookRecords.AddRangeAsync(records);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Books.Should().NotBeEmpty();
            result.Books.All(b => b.UserId == _testQuery.User.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Records_When_Search_Query_Is_Empty()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var request = new BookListGetQuery(1, 0, string.Empty, _testQuery.User);

            var result = await _handler.Handle(request);

            result.Books.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Records_When_There_Is_No_UserId()
        {
            _testQuery.User.Id = string.Empty;

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Books.Should().NotBeNullOrEmpty();
            result.Books.Should().Contain(x => x.UserId != _testQuery.User.Id);
        }

        [Fact]
        public async Task Handler_Returns_Requested_Count()
        {
            await _context.BookRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var request = new BookListGetQuery(1, 0, _testQuery.SearchQuery, _testQuery.User);

            var result = await _handler.Handle(request);

            result.Books.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handler_Returns_Total_Books()
        {
            await InitializeRecords();

            var count = await _context.Books.CountAsync();

            var query = new BookListGetQuery(10, 0, _testQuery.SearchQuery, null);

            var result = await _handler.Handle(query);

            result.TotalCount.Should().Be(count);
        }

        [Fact]
        public async Task Handler_Returns_Total_Books_For_User()
        {
            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(b => b.UserId, "not this user")
                .Create();

            await InitializeRecords();

            var count = await _context.Books.CountAsync(b => b.UserId == _testQuery.User.Id);

            var result = await _handler.Handle(_testQuery);

            result.TotalCount.Should().Be(count);
        }

        [Fact]
        public async Task Handler_Sorts_By_Author()
        {
            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Bendis")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            var expectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Archibald")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(unexpectedBook);
            await _context.BookRecords.AddAsync(expectedBook);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.Books.First().Author.Should().Be(expectedBook.Author);
        }

        [Fact]
        public async Task Handler_Sorts_By_Author_Then_Title()
        {
            var unexpectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Wonder Woman")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            var expectedBook = _fixture
                .Build<BookRecord>()
                .With(book => book.Author, "Rucka")
                .With(book => book.Title, "Old Guard")
                .With(book => book.UserId, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(unexpectedBook);
            await _context.BookRecords.AddAsync(expectedBook);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.Books.First().Author.Should().Be(expectedBook.Author);
        }

        private async Task InitializeRecords()
        {
            await _context.BookRecords.AddRangeAsync(_bookRecords);

            await _context.SaveChangesAsync();
        }
    }
}