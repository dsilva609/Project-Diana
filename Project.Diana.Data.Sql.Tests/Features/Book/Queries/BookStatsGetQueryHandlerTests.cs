using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Queries
{
    public class BookStatsGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly BookStatsGetQueryHandler _handler;
        private readonly BookStatsGetQuery _testQuery;

        public BookStatsGetQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = new BookStatsGetQuery();

            _handler = new BookStatsGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Gets_Book_Count()
        {
            await InitializeRecords();

            var results = await _handler.Handle(_testQuery);

            results.BookCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Book_Count_For_User()
        {
            var testQuery = new BookStatsGetQuery(1);

            var books = _fixture
                .Build<BookRecord>()
                .With(b => b.UserNum, testQuery.UserId)
                .CreateMany();

            var notIncludedBook = _fixture
                .Build<BookRecord>()
                .With(b => b.UserNum, testQuery.UserId + 1)
                .Create();

            await _context.AddRangeAsync(books);

            await _context.AddAsync(notIncludedBook);

            await _context.SaveChangesAsync();

            var results = await _handler.Handle(testQuery);

            results.BookCount.Should().Be(books.Count());
        }

        [Fact]
        public async Task Handler_Gets_Comic_Book_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.Type, BookTypeReference.Comic)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();

            var results = await _handler.Handle(_testQuery);

            results.ComicCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Completed_Book_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.CompletionStatus, CompletionStatusReference.Completed)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();

            var results = await _handler.Handle(_testQuery);

            results.CompletedBookCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_In_Progress_Book_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.CompletionStatus, CompletionStatusReference.InProgress)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();
            var results = await _handler.Handle(_testQuery);

            results.InProgressBookCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Manga_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.Type, BookTypeReference.Manga)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();

            var results = await _handler.Handle(_testQuery);

            results.MangaCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Not_Started_Book_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.CompletionStatus, CompletionStatusReference.NotStarted)
                .Create();

            await _context.BookRecords.AddAsync(book);

            var results = await _handler.Handle(_testQuery);

            results.NotStartedBookCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Novel_Count()
        {
            await InitializeRecords();

            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.Type, BookTypeReference.Novel)
                .Create();

            await _context.BookRecords.AddAsync(book);

            var results = await _handler.Handle(_testQuery);

            results.NovelCount.Should().BeGreaterThan(0);
        }

        private async Task InitializeRecords()
        {
            await _context.BookRecords.AddAsync(_fixture.Create<BookRecord>());

            await _context.SaveChangesAsync();
        }
    }
}