using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish;
using Project.Diana.WebApi.Features.Wish.WishList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.WishList
{
    public class WishGetListByUserIDRequestHandlerTests
    {
        private readonly WishGetListByUserIDRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly WishGetListByUserIDRequest _testRequest;

        public WishGetListByUserIDRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<WishGetListByUserIDRequest>();

            InitializeHandlerResult();

            _handler = new WishGetListByUserIDRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_QueryDispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(It.IsNotNull<WishGetListByUserIDQuery>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_List()
        {
            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.AlbumWishes.Should().NotBeNullOrEmpty();
            result.BookWishes.Should().NotBeNullOrEmpty();
            result.GameWishes.Should().NotBeNullOrEmpty();
            result.MovieWishes.Should().NotBeNullOrEmpty();
        }

        private void InitializeHandlerResult()
        {
            var wishes = new List<WishRecord>
            {
                new WishRecord{ ItemType = ItemReference.Album, Category = "Album"},
                new WishRecord{ItemType = ItemReference.Book, Category = "Book"},
                new WishRecord{ItemType = ItemReference.Game, Category = "Game"},
                new WishRecord{ItemType = ItemReference.Movie, Category = "Movie"}
            };

            _queryDispatcher
                .Setup(x =>
                    x.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(
                        It.IsNotNull<WishGetListByUserIDQuery>()))
                .ReturnsAsync(wishes);
        }
    }
}