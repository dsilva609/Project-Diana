using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Commands
{
    public class AlbumSubmissionCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly AlbumSubmissionCommandHandler _handler;
        private readonly AlbumSubmissionCommand _testCommand;

        public AlbumSubmissionCommandHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            var config = new MapperConfiguration(x => x.AddProfile<AlbumMappingProfile>());
            var mapper = new Mapper(config);

            _testCommand = fixture.Create<AlbumSubmissionCommand>();

            _handler = new AlbumSubmissionCommandHandler(_context, mapper);
        }

        [Fact]
        public async Task Handler_Adds_Record()
        {
            await _handler.Handle(_testCommand);

            _context.Albums.Any(a => a.Title == _testCommand.Title && a.Artist == _testCommand.Artist).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Does_Not_Add_Record_With_Same_Artist_Media_Type_And_Title_For_User()
        {
            var existingAlbum = new AlbumRecord
            {
                Artist = _testCommand.Artist,
                MediaType = _testCommand.MediaType,
                Title = _testCommand.Title,
                UserID = _testCommand.User.Id
            };

            await _context.Albums.AddAsync(existingAlbum);

            await _context.SaveChangesAsync();

            await _handler.Handle(_testCommand);

            var album = await _context.Albums.CountAsync(a
                => a.Artist == _testCommand.Artist
                   && a.MediaType == _testCommand.MediaType
                   && a.Title == _testCommand.Title
                   && a.UserID == _testCommand.User.Id);

            album.Should().Be(1);
        }

        [Fact]
        public async Task Handler_Sets_Added_Time()
        {
            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.DateAdded.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task Handler_Sets_Completion_Status_To_Complete_If_Completion_Count_Is_Greater_Than_Zero()
        {
            var command = new AlbumSubmissionCommand(
                _testCommand.Artist,
            _testCommand.Category,
                _testCommand.CompletionStatus,
                _testCommand.CountryOfOrigin,
                _testCommand.CountryPurchased,
                _testCommand.DatePurchased,
                _testCommand.DiscogsId,
                _testCommand.Genre,
                _testCommand.ImageUrl,
                _testCommand.IsNew,
                _testCommand.IsPhysical,
                _testCommand.LocationPurchased,
                _testCommand.MediaType,
                _testCommand.Notes,
                _testCommand.RecordLabel,
                _testCommand.Size,
                _testCommand.Speed,
                _testCommand.Style,
                1,
                _testCommand.Title,
                _testCommand.YearReleased,
                _testCommand.User
                );

            await _handler.Handle(command);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.CompletionStatus.Should().Be(CompletionStatusReference.Completed);
        }

        [Fact]
        public async Task Handler_Sets_Date_Added_And_Date_Updated_To_The_Same_Date()
        {
            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.DateAdded.Should().Be(album.DateUpdated);
        }

        [Fact]
        public async Task Handler_Sets_Date_Modified()
        {
            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.DateUpdated.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task Handler_Sets_User_Id()
        {
            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.UserID.Should().Be(_testCommand.User.Id);
        }

        [Fact]
        public async Task Handler_Sets_User_Num()
        {
            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Artist == _testCommand.Artist && a.Title == _testCommand.Title);

            album.UserNum.Should().Be(_testCommand.User.UserNum);
        }
    }
}