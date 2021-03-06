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
using Project.Diana.WebApi.Features.Wish.WishList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.WishList
{
    public class WishGetListByUserIdRequestHandlerTests
    {
        private readonly WishGetListByUserIdRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly WishGetListByUserIdRequest _testRequest;

        public WishGetListByUserIdRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<WishGetListByUserIdRequest>();

            InitializeHandlerResult();

            _handler = new WishGetListByUserIdRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_QueryDispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<WishGetListByUserIdQuery, IEnumerable<WishRecord>>(It.IsNotNull<WishGetListByUserIdQuery>()), Times.Once);
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
                    x.Dispatch<WishGetListByUserIdQuery, IEnumerable<WishRecord>>(
                        It.IsNotNull<WishGetListByUserIdQuery>()))
                .ReturnsAsync(wishes);
        }
    }
}