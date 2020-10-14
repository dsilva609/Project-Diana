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
    public class AlbumAddToShowcaseCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly ProjectDianaWriteContext _context;
        private readonly AlbumAddToShowcaseCommandHandler _handler;
        private readonly AlbumRecord _testAlbum;
        private readonly AlbumAddToShowcaseCommand _testCommand;

        public AlbumAddToShowcaseCommandHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            _testCommand = fixture.Create<AlbumAddToShowcaseCommand>();
            _testAlbum = fixture
                .Build<AlbumRecord>()
                .With(a => a.ID, _testCommand.AlbumId)
                .With(a => a.IsShowcased, false)
                .With(a => a.UserID, _testCommand.User.Id)
                .Create();

            _handler = new AlbumAddToShowcaseCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_Already_In_Showcase()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;
            _testAlbum.IsShowcased = true;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().Be(lastModifiedTime);
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
        public async Task Handler_Does_Not_Update_Album_With_Not_Matching_AlbumId()
        {
            var lastModifiedTime = _testAlbum.DateUpdated;
            _testAlbum.ID = _testCommand.AlbumId + 1;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.DateUpdated.Should().Be(lastModifiedTime);
        }

        [Fact]
        public async Task Handler_Sets_Album_To_Showcased()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testAlbum.IsShowcased.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Update_Modified_Time()
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