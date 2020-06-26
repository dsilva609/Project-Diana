using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryHandlerTests
    {
        private readonly WishGetByIDQueryHandler handler;
        private readonly WishGetByIDQuery testQuery;

        public WishGetByIDQueryHandlerTests()
        {
            var fixture = new Fixture();

            testQuery = fixture.Create<WishGetByIDQuery>();

            handler = new WishGetByIDQueryHandler();
        }

        [Fact]
        public void HandlerRetrievesWish()
        {
            var result = handler.Handle(testQuery);

            result.Should().NotBeNull();
        }
    }
}