using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Commands
{
    public class AlbumClearShowcaseCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly ProjectDianaWriteContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumClearShowcaseCommandHandler _handler;
        private readonly AlbumClearShowcaseCommand _testCommand;
        private readonly IEnumerable<AlbumRecord> _testRecords;

        public AlbumClearShowcaseCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = _fixture.Create<AlbumClearShowcaseCommand>();
            _testRecords = _fixture
                .Build<AlbumRecord>()
                .With(a => a.DateUpdated, DateTime.UtcNow)
                .With(a => a.IsShowcased, true)
                .With(a => a.UserID, _testCommand.User.Id)
                .CreateMany();

            _handler = new AlbumClearShowcaseCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Album_For_Different_User()
        {
            var testAlbum = _fixture
                .Build<AlbumRecord>()
                .With(a => a.IsShowcased, true)
                .With(a => a.UserID, $"{_testCommand.User.Id}not matching")
                .Create();

            await _context.Albums.AddAsync(testAlbum);

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            testAlbum.IsShowcased.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Does_Not_Update_Non_Showcased_Albums()
        {
            var testAlbum = _fixture
                .Build<AlbumRecord>()
                .With(a => a.IsShowcased, false)
                .With(a => a.UserID, _testCommand.User.Id)
                .Create();

            await _context.Albums.AddAsync(testAlbum);

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            testAlbum.IsShowcased.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_Sets_Albums_To_Not_Showcased()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecords.All(a => !a.IsShowcased).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Updates_DateUpdated()
        {
            var lastModifiedTime = _testRecords.FirstOrDefault()?.DateUpdated;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            _testRecords.All(a => a.DateUpdated > lastModifiedTime).Should().BeTrue();
        }

        private async Task InitializeRecords()
        {
            await _context.Albums.AddRangeAsync(_testRecords);

            await _context.SaveChangesAsync();
        }
    }
}