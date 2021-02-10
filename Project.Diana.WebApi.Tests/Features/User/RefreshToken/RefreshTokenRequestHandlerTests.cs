using Moq;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.User.RefreshToken;

namespace Project.Diana.WebApi.Tests.Features.User.RefreshToken
{
    public class RefreshTokenRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly RefreshTokenRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;

        public RefreshTokenRequestHandlerTests()
        {
            _commandDispatcher = new Mock<ICommandDispatcher>();
            _queryDispatcher = new Mock<IQueryDispatcher>();

            _handler = new RefreshTokenRequestHandler(_commandDispatcher.Object, _queryDispatcher.Object);
        }
    }
}