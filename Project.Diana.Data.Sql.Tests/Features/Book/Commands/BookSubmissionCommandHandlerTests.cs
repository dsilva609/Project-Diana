using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Book.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Book.Commands
{
    public class BookSubmissionCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly BookSubmissionCommandHandler _handler;
        private readonly BookSubmissionCommand _testCommand;

        public BookSubmissionCommandHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            var config = new MapperConfiguration(x => x.AddProfile<BookMappingProfile>());
            var mapper = new Mapper(config);

            _testCommand = fixture.Create<BookSubmissionCommand>();

            _handler = new BookSubmissionCommandHandler(_context, mapper);
        }

        [Fact]
        public async Task Handler_Adds_Record()
        {
            await _handler.Handle(_testCommand);

            _context.Books.Any(a => a.Title == _testCommand.Title && a.Author == _testCommand.Author).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Does_Not_Add_Record_With_Same_Artist_Media_Type_And_Title_For_User()
        {
            var existingBook = new BookRecord
            {
                Author = _testCommand.Author,
                Type = _testCommand.Type,
                Title = _testCommand.Title,
                UserID = _testCommand.User.Id
            };

            await _context.Books.AddAsync(existingBook);

            await _context.SaveChangesAsync();

            await _handler.Handle(_testCommand);

            var Book = await _context.Books.CountAsync(a
                => a.Author == _testCommand.Author
                   && a.Type == _testCommand.Type
                   && a.Title == _testCommand.Title
                   && a.UserID == _testCommand.User.Id);

            Book.Should().Be(1);
        }

        [Fact]
        public async Task Handler_Sets_Added_Time()
        {
            await _handler.Handle(_testCommand);

            var Book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            Book.DateAdded.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task Handler_Sets_Completion_Status_To_Complete_If_Completion_Count_Is_Greater_Than_Zero()
        {
            var command = new BookSubmissionCommand(
                _testCommand.Author,
                _testCommand.Category,
                _testCommand.CompletionStatus,
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
                _testCommand.TimesCompleted,
                _testCommand.Title,
                _testCommand.Type,
                _testCommand.YearReleased,
                _testCommand.User
                );

            await _handler.Handle(command);

            var book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            book.CompletionStatus.Should().Be(CompletionStatusReference.Completed);
        }

        [Fact]
        public async Task Handler_Sets_Date_Added_And_Date_Updated_To_The_Same_Date()
        {
            await _handler.Handle(_testCommand);

            var Book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            Book.DateAdded.Should().Be(Book.DateUpdated);
        }

        [Fact]
        public async Task Handler_Sets_Date_Modified()
        {
            await _handler.Handle(_testCommand);

            var Book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            Book.DateUpdated.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task Handler_Sets_User_Id()
        {
            await _handler.Handle(_testCommand);

            var Book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            Book.UserID.Should().Be(_testCommand.User.Id);
        }

        [Fact]
        public async Task Handler_Sets_User_Num()
        {
            await _handler.Handle(_testCommand);

            var Book = await _context.Books.FirstOrDefaultAsync(a => a.Author == _testCommand.Author && a.Title == _testCommand.Title);

            Book.UserNum.Should().Be(_testCommand.User.UserNum);
        }
    }
}