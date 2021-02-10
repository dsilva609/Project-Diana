using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.User.RefreshToken;
using Project.Diana.WebApi.Helpers.Token;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.User.RefreshToken
{
    public class RefreshTokenRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly RefreshTokenRecord _existingRefreshToken;
        private readonly IFixture _fixture;
        private readonly RefreshTokenRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly RefreshTokenRequest _testRequest;
        private readonly Mock<ITokenService> _tokenService;

        public RefreshTokenRequestHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _queryDispatcher = new Mock<IQueryDispatcher>();
            _tokenService = new Mock<ITokenService>();

            _testRequest = _fixture.Create<RefreshTokenRequest>();

            _existingRefreshToken = _fixture
                 .Build<RefreshTokenRecord>()
                 .With(t => t.ExpiresOn, DateTimeOffset.UtcNow.AddDays(1))
                 .With(t => t.Token, _testRequest.RefreshToken)
                 .Create();

            var user = _fixture
                .Build<ApplicationUser>()
                .With(u => u.RefreshTokens, new[] { _existingRefreshToken })
                .Create();

            _queryDispatcher
                .Setup(x => x.Dispatch<UserGetByRefreshTokenQuery, Result<ApplicationUser>>(It.Is<UserGetByRefreshTokenQuery>(q => q != null)))
                .ReturnsAsync(Result.Success(user));

            _tokenService
                .Setup(x => x.GenerateAccessToken(It.IsAny<ApplicationUser>()))
                .Returns("token");

            _tokenService
                .Setup(x => x.GenerateRefreshToken(It.IsAny<ApplicationUser>()))
                .Returns(_fixture.Create<RefreshTokenRecord>());

            _handler = new RefreshTokenRequestHandler(_commandDispatcher.Object, _queryDispatcher.Object, _tokenService.Object);
        }

        [Fact]
        public async Task Handler_Clears_Expired_Tokens_For_User()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.Is<RefreshTokenClearExpiredForUserCommand>(c => c != null && c.ActiveTokenForExpiration == _existingRefreshToken.Token && c.UserId == _existingRefreshToken.UserId)), Times.Once());
        }

        [Fact]
        public async Task Handler_Generates_Access_Token()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _tokenService.Verify(x => x.GenerateAccessToken(It.Is<ApplicationUser>(u => u != null)), Times.Once);
        }

        [Fact]
        public async Task Handler_Generates_Refresh_Token()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _tokenService.Verify(x => x.GenerateRefreshToken(It.Is<ApplicationUser>(u => u != null)), Times.Once);
        }

        [Fact]
        public async Task Handler_Gets_User_By_Refresh_Token()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<UserGetByRefreshTokenQuery, Result<ApplicationUser>>(It.Is<UserGetByRefreshTokenQuery>(q => q != null)), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_Unauthorized_If_Token_Is_Expired()
        {
            var token = _fixture
                .Build<RefreshTokenRecord>()
                .With(t => t.ExpiresOn, DateTimeOffset.UtcNow.AddDays(-1))
                .With(t => t.Token, _testRequest.RefreshToken)
                .Create();

            var user = _fixture
                .Build<ApplicationUser>()
                .With(u => u.RefreshTokens, new[] { token })
                .Create();

            _queryDispatcher
                .Setup(x => x.Dispatch<UserGetByRefreshTokenQuery, Result<ApplicationUser>>(It.Is<UserGetByRefreshTokenQuery>(q => q != null)))
                .ReturnsAsync(Result.Success(user));

            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Handler_Returns_Unauthorized_When_Unable_To_Find_User()
        {
            _queryDispatcher
                .Setup(x => x.Dispatch<UserGetByRefreshTokenQuery, Result<ApplicationUser>>(It.Is<UserGetByRefreshTokenQuery>(q => q != null)))
                .ReturnsAsync(Result.Failure<ApplicationUser>("failure"));

            var result = await _handler.Handle(_testRequest, CancellationToken.None);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Handler_Saves_Refresh_Token()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.Is<RefreshTokenCreateCommand>(c => c != null)), Times.Once);
        }
    }
}