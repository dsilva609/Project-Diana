﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Queries
{
    public class AlbumListGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly IEnumerable<AlbumRecord> _albumRecords;
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumListGetQueryHandler _handler;
        private readonly AlbumListGetQuery _testQuery;
        private readonly AlbumRecord _testRecord;

        public AlbumListGetQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _albumRecords = _fixture.Create<IEnumerable<AlbumRecord>>();
            _testQuery = new AlbumListGetQuery(13, _fixture.Create<ApplicationUser>());
            _testRecord = _fixture
                .Build<AlbumRecord>()
                .With(album => album.UserID, _testQuery.User.Id)
                .Create();

            _handler = new AlbumListGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Album_For_UserID()
        {
            await _context.AlbumRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
            result.All(a => a.UserID == _testQuery.User.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Albums()
        {
            await _context.AlbumRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Records_When_There_Is_No_UserID()
        {
            _testQuery.User.Id = string.Empty;

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
            result.Should().Contain(x => x.UserID != _testQuery.User.Id);
        }

        [Fact]
        public async Task Handler_Returns_Requested_Count()
        {
            await _context.AlbumRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var request = new AlbumListGetQuery(1, _testQuery.User);

            var result = await _handler.Handle(request);

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handler_Sorts_By_Artist()
        {
            var unexpectedAlbum = _fixture
                .Build<AlbumRecord>()
                .With(album => album.Artist, "Megadeth")
                .With(album => album.UserID, _testQuery.User.Id)
                .Create();

            var expectedAlbum = _fixture
               .Build<AlbumRecord>()
               .With(album => album.Artist, "Dio")
               .With(album => album.UserID, _testQuery.User.Id)
               .Create();

            await _context.AlbumRecords.AddAsync(unexpectedAlbum);
            await _context.AlbumRecords.AddAsync(expectedAlbum);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.First().Artist.Should().Be(expectedAlbum.Artist);
        }

        [Fact]
        public async Task Handler_Sorts_By_Artist_Then_By_Title()
        {
            var unexpectedAlbum = _fixture
                .Build<AlbumRecord>()
                .With(album => album.Artist, "Black Sabbath")
                .With(album => album.Title, "Mob Rules")
                .With(album => album.UserID, _testQuery.User.Id)
                .Create();

            var expectedAlbum = _fixture
                .Build<AlbumRecord>()
                .With(album => album.Artist, "Black Sabbath")
                .With(album => album.Title, "Heaven and Hell")
                .With(album => album.UserID, _testQuery.User.Id)
                .Create();

            await _context.AlbumRecords.AddAsync(unexpectedAlbum);
            await _context.AlbumRecords.AddAsync(expectedAlbum);
            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.First().Artist.Should().Be(expectedAlbum.Artist);
        }

        private async Task InitializeRecords()
        {
            await _context.AlbumRecords.AddRangeAsync(_albumRecords);

            await _context.SaveChangesAsync();
        }
    }
}