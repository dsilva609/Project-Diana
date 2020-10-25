using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Showcase.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Showcase.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Showcase.Queries
{
    public class ShowcaseListGetListQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly ShowcaseGetListQueryHandler _handler;
        private readonly ShowcaseGetListQuery _testQuery;

        public ShowcaseListGetListQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();
            _testQuery = _fixture.Create<ShowcaseGetListQuery>();

            _handler = new ShowcaseGetListQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Items_For_User()
        {
            var albumForOtherUser = _fixture
                .Build<AlbumRecord>()
                .With(a => a.IsShowcased, true)
                .Create();

            var bookForOtherUser = _fixture
                .Build<BookRecord>()
                .With(a => a.IsShowcased, true)
                .Create();

            await _context.AlbumRecords.AddAsync(albumForOtherUser);

            await _context.BookRecords.AddAsync(bookForOtherUser);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.ShowcasedAlbums.All(album => album.UserNum == _testQuery.UserID).Should().BeTrue();
            result.ShowcasedAlbums.Should().NotContain(a => a.ID == albumForOtherUser.ID);

            result.ShowcasedBooks.All(book => book.UserNum == _testQuery.UserID).Should().BeTrue();
            result.ShowcasedBooks.Should().NotContain(b => b.ID == bookForOtherUser.ID);
        }

        [Fact]
        public async Task Handler_Returns_Response()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.ShowcasedAlbums.Should().NotBeEmpty();
            result.ShowcasedAlbums.All(album => album.IsShowcased).Should().BeTrue();
            result.ShowcasedBooks.Should().NotBeEmpty();
            result.ShowcasedBooks.All(book => book.IsShowcased).Should().BeTrue();
        }

        private async Task InitializeRecords()
        {
            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.IsShowcased, true)
                .With(a => a.UserNum, _testQuery.UserID)
                .Create();

            var book = _fixture
                .Build<BookRecord>()
                .With(a => a.IsShowcased, true)
                .With(a => a.UserNum, _testQuery.UserID)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.BookRecords.AddAsync(book);

            await _context.SaveChangesAsync();
        }
    }
}