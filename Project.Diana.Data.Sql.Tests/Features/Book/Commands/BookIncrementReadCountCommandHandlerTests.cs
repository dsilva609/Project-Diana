using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Commands
{
    public class BookIncrementReadCountCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly ProjectDianaWriteContext _context;
        private readonly BookIncrementReadCountCommandHandler _handler;
        private readonly BookIncrementReadCountCommand _testCommand;
        private readonly BookRecord _testRecord;

        public BookIncrementReadCountCommandHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            _testCommand = fixture.Create<BookIncrementReadCountCommand>();
            _testRecord = fixture
                .Build<BookRecord>()
                .With(a => a.ID, _testCommand.BookId)
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.UserID, _testCommand.User.Id)
                .Create();

            _handler = new BookIncrementReadCountCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_For_Non_Matching_Book_Id()
        {
            var lastModifiedTime = _testRecord.DateUpdated;
            _testRecord.ID++;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Book_For_Non_Matching_User()
        {
            var lastModifiedTime = _testRecord.DateUpdated;
            _testRecord.UserID = $"{_testCommand.User.Id}non matching";

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Increments_Play_Count()
        {
            await InitializeRecords();

            var previousPlayCount = _testRecord.TimesCompleted;

            await _handler.Handle(_testCommand);

            _testRecord.TimesCompleted.Should().Be(previousPlayCount + 1);
        }

        [Fact]
        public async Task Handler_Sets_Item_Status_To_Completed()
        {
            _testRecord.CompletionStatus = CompletionStatusReference.NotStarted;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.CompletionStatus.Should().Be(CompletionStatusReference.Completed);
        }

        [Fact]
        public async Task Handler_Sets_Last_Completed_And_Modified_Time_To_Same_Date()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.DateUpdated.Should().BeSameDateAs(_testRecord.LastCompleted);
        }

        [Fact]
        public async Task Handler_Update_Last_Completed_Time()
        {
            var lastModifiedTime = _testRecord.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.LastCompleted.Should().BeAfter(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Updates_Modified_Time()
        {
            var lastModifiedTime = _testRecord.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecord.DateUpdated.Should().BeAfter(lastModifiedTime);
        }

        private async Task InitializeRecords()
        {
            await _context.Books.AddAsync(_testRecord);

            await _context.SaveChangesAsync();
        }
    }
}