using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryHandlerTests
    {
        private readonly WishGetByIDQueryHandler _handler;
        private readonly Mock<IProjectDianaContext> _projectDianaContext;
        private readonly WishGetByIDQuery _testQuery;

        public WishGetByIDQueryHandlerTests()
        {
            var fixture = new Fixture();

            _projectDianaContext = new Mock<IProjectDianaContext>();
            _testQuery = fixture.Create<WishGetByIDQuery>();

            _handler = new WishGetByIDQueryHandler(_projectDianaContext.Object);
        }

        [Fact]
        public void HandlerRetrievesWish()
        {
            var result = _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }
    }
}