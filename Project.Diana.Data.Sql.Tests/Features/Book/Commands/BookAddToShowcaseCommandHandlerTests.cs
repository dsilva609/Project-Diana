using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Commands
{
    public class BookAddToShowcaseCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly BookAddToShowcaseCommandHandler _handler;
        private readonly BookRecord _testBook;
        private readonly BookAddToShowcaseCommand _testCommand;

        public BookAddToShowcaseCommandHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = fixture.Create<BookAddToShowcaseCommand>();
            _testBook = fixture
                .Build<BookRecord>()
                .With(a => a.Id, _testCommand.BookId)
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.IsShowcased, false)
                .With(a => a.UserId, _testCommand.User.Id)
                .Create();

            _handler = new BookAddToShowcaseCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_Already_In_Showcase()
        {
            var lastModifiedTime = _testBook.DateUpdated;
            _testBook.IsShowcased = true;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testBook.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_For_Non_Matching_User_Id()
        {
            var lastModifiedTime = _testBook.DateUpdated;
            _testBook.UserId = $"{_testCommand.User.Id}not matching";

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testBook.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_With_Not_Matching_BookId()
        {
            var lastModifiedTime = _testBook.DateUpdated;
            _testBook.Id = _testCommand.BookId + 1;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testBook.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Sets_Book_To_Showcased()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testBook.IsShowcased.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Updates_Modified_Time()
        {
            var lastModifiedTime = _testBook.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var book = await _context.Books.FirstOrDefaultAsync(a => a.Id == _testCommand.BookId);

            book.DateUpdated.Should().BeAfter(lastModifiedTime);
        }

        private async Task InitializeRecords()
        {
            await _context.Books.AddAsync(_testBook);

            await _context.SaveChangesAsync();
        }
    }
}