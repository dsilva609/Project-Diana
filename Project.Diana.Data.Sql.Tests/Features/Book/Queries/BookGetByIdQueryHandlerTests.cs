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
    public class BookGetByIdQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly BookGetByIdQueryHandler _handler;
        private readonly BookGetByIdQuery _testQuery;

        public BookGetByIdQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = _fixture.Create<BookGetByIdQuery>();

            _handler = new BookGetByIdQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Book()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_Returns_Book_For_User_Id()
        {
            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.ID, _testQuery.ID)
                .With(b => b.UserID, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
            result.UserID.Should().Be(book.UserID);
        }

        [Fact]
        public async Task Handler_Returns_Book_When_User_Id_Is_Missing()
        {
            await InitializeRecords();

            var query = new BookGetByIdQuery(_testQuery.ID, null);

            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
        }

        private async Task InitializeRecords()
        {
            var book = _fixture
                .Build<BookRecord>()
                .With(b => b.ID, _testQuery.ID)
                .With(b => b.UserID, _testQuery.User.Id)
                .Create();

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();
        }
    }
}