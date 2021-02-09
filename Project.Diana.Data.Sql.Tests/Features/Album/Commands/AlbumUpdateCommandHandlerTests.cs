using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Commands
{
    public class AlbumUpdateCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumUpdateCommandHandler _handler;
        private readonly AlbumRecord _testAlbum;
        private readonly AlbumUpdateCommand _testCommand;

        public AlbumUpdateCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = _fixture.Create<AlbumUpdateCommand>();
            _testAlbum = _fixture
                .Build<AlbumRecord>()
                .With(a => a.ID, _testCommand.AlbumId)
                .With(a => a.Artist, _testCommand.Artist)
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.MediaType, _testCommand.MediaType)
                .With(a => a.TimesCompleted, 1)
                .With(a => a.Title, _testCommand.Title)
                .With(a => a.UserID, _testCommand.User.Id)
                .Create();

            _handler = new AlbumUpdateCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_For_Different_User_Id()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.AlbumId + 1, CompletionStatusReference.NotStarted, 0, $"{_testCommand.User.Id}not found");

            await _handler.Handle(command);

            _testAlbum.DateUpdated.Should().Be(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_When_Another_Exists_For_User()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.AlbumId + 1, CompletionStatusReference.Completed, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testAlbum.DateUpdated.Should().BeSameDateAs(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_When_Id_Is_Not_Found()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            _testAlbum.Artist = "not found";

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.AlbumId + 1, CompletionStatusReference.NotStarted, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testAlbum.DateUpdated.Should().Be(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Sets_Play_Count_For_Completed_Album()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            _testAlbum.TimesCompleted = 0;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.AlbumId, CompletionStatusReference.Completed, 0, _testCommand.User.Id);

            await _handler.Handle(command);

            _testAlbum.TimesCompleted.Should().Be(1);
            _testAlbum.LastCompleted.Should().BeAfter(lastModifiedDate);
            _testAlbum.DateUpdated.Should().BeAfter(lastModifiedDate);
            _testAlbum.LastCompleted.Should().BeSameDateAs(_testAlbum.DateUpdated);
        }

        [Fact]
        public async Task Handler_Updates_Album()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().BeAfter(lastModifiedDate);
        }

        [Fact]
        public async Task Handler_Updates_Completion_Status()
        {
            var lastModifiedDate = _testAlbum.DateUpdated;

            _testAlbum.CompletionStatus = CompletionStatusReference.NotStarted;

            await InitializeRecords();

            var command = CreateTestCommand(_testCommand.AlbumId, CompletionStatusReference.NotStarted, 1, _testCommand.User.Id);

            await _handler.Handle(command);

            _testAlbum.CompletionStatus.Should().Be(CompletionStatusReference.Completed);
            _testAlbum.LastCompleted.Should().BeAfter(lastModifiedDate);
            _testAlbum.DateUpdated.Should().BeAfter(lastModifiedDate);
            _testAlbum.LastCompleted.Should().BeSameDateAs(_testAlbum.DateUpdated);
        }

        private AlbumUpdateCommand CreateTestCommand(
            int albumId,
            CompletionStatusReference completionStatus,
            int timesCompleted,
            string userId) =>
            new AlbumUpdateCommand(
                albumId,
                _testCommand.Artist,
                _testCommand.Category,
                completionStatus,
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
                timesCompleted,
                _testCommand.Title,
                _testCommand.YearReleased,
                new ApplicationUser { Id = userId });

        private async Task InitializeRecords()
        {
            await _context.Albums.AddAsync(_testAlbum);

            await _context.SaveChangesAsync();
        }
    }
}