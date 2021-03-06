using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Commands
{
    public class BookUpdateCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly IFixture _fixture;
        private readonly BookUpdateCommandHandler _handler;
        private readonly BookRecord _testBook;
        private readonly BookUpdateCommand _testCommand;

        public BookUpdateCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = _fixture.Create<BookUpdateCommand>();
            _testBook = _fixture
                .Build<BookRecord>()
                .With(a => a.Id, _testCommand.BookId)
                .With(a => a.Author, _testCommand.Author)
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.Type, _testCommand.Type)
                .With(a => a.TimesCompleted, 1)
                .With(a => a.Title, _testCommand.Title)
                .With(a => a.UserId, _testCommand.User.Id)
                .Create();

            _handler = new BookUpdateCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_For_Different_User_Id()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.BookId + 1, CompletionStatusReference.NotStarted, 0, $"{_testCommand.User.Id}not found");

            await _handler.Handle(command);

            _testBook.DateUpdated.Should().Be(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_When_Another_Exists_For_User()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.BookId + 1, CompletionStatusReference.Completed, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testBook.DateUpdated.Should().BeSameDateAs(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_When_Id_Is_Not_Found()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            _testBook.Author = "not found";

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.BookId + 1, CompletionStatusReference.NotStarted, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testBook.DateUpdated.Should().Be(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Sets_Play_Count_For_Completed_Book()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            _testBook.TimesCompleted = 0;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.BookId, CompletionStatusReference.Completed, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testBook.TimesCompleted.Should().Be(1);
            _testBook.LastCompleted.Should().BeAfter(lastModifiedDate);
            _testBook.DateUpdated.Should().BeAfter(lastModifiedDate);
            _testBook.LastCompleted.Should().BeSameDateAs(_testBook.DateUpdated);
        }

        [Fact]
        public async Task Handler_Updates_Book()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testBook.DateUpdated.Should().BeAfter(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Updates_Completion_Status()
        {
            var lastModifiedDate = _testBook.DateUpdated;

            _testBook.CompletionStatus = CompletionStatusReference.NotStarted;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.BookId, CompletionStatusReference.NotStarted, 1, _testCommand.User.Id);

            await _handler.Handle(command);

            _testBook.CompletionStatus.Should().Be(CompletionStatusReference.Completed);
            _testBook.LastCompleted.Should().BeAfter(lastModifiedDate);
            _testBook.DateUpdated.Should().BeAfter(lastModifiedDate);
            _testBook.LastCompleted.Should().BeSameDateAs(_testBook.DateUpdated);
        }

        private BookUpdateCommand CreateTestCommand(
            int bookId,
            CompletionStatusReference completionStatus,
            int timesCompleted,
            string userId) =>
            new BookUpdateCommand(
                _testCommand.Author,
                bookId,
                _testCommand.Category,
                completionStatus,
                _testCommand.CountryOfOrigin,
                _testCommand.CountryPurchased,
                _testCommand.DatePurchased,
                _testCommand.Genre,
                _testCommand.ImageUrl,
                _testCommand.ISBN10,
                _testCommand.ISBN13,
                _testCommand.IsFirstEdition,
                _testCommand.IsHardcover,
                _testCommand.IsNewPurchase,
                _testCommand.IsPhysical,
                _testCommand.IsReissue,
                _testCommand.Language,
                _testCommand.LocationPurchased,
                _testCommand.Notes,
                _testCommand.PageCount,
                _testCommand.Publisher,
                timesCompleted,
                _testCommand.Title,
                _testCommand.Type,
                _testCommand.YearReleased,
                new ApplicationUser { Id = userId });

        private async Task InitializeRecords()
        {
            await _context.Books.AddAsync(_testBook);

            await _context.SaveChangesAsync();
        }
    }
}