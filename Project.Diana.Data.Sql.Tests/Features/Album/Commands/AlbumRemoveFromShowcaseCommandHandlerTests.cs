using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Commands
{
    public class AlbumRemoveFromShowcaseCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly ProjectDianaWriteContext _context;
        private readonly AlbumRemoveFromShowcaseCommandHandler _handler;
        private readonly AlbumRecord _testAlbum;
        private readonly AlbumRemoveFromShowcaseCommand _testCommand;

        public AlbumRemoveFromShowcaseCommandHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = fixture.Create<AlbumRemoveFromShowcaseCommand>();
            _testAlbum = fixture
                .Build<AlbumRecord>()
                .With(a => a.ID, _testCommand.AlbumId)
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.IsShowcased, true)
                .With(a => a.UserID, _testCommand.User.Id)
                .Create();

            _handler = new AlbumRemoveFromShowcaseCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_For_Non_Matching_User_Id()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;
            _testAlbum.UserID = $"{_testCommand.User.Id}not matching";

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_With_Non_Matching_AlbumId()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;
            _testAlbum.ID = _testCommand.AlbumId + 1;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_When_Album_Is_Already_Not_Showcased()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;
            _testAlbum.IsShowcased = false;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Sets_Album_To_Not_Showcased()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.IsShowcased.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_Updates_Modified_Time()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var album = await _context.Albums.FirstOrDefaultAsync(a => a.ID == _testCommand.AlbumId);

            album.DateUpdated.Should().BeAfter(lastModifiedTime);
        }

        private async Task InitializeRecords()
        {
            await _context.Albums.AddAsync(_testAlbum);

            await _context.SaveChangesAsync();
        }
    }
}