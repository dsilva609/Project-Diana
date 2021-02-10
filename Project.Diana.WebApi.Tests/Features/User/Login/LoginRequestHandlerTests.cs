using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.User.Login;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.Diana.WebApi.Tests.Features.User.Login
{
    public class LoginRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly LoginRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
        private readonly LoginRequest _testLoginRequest;
        private readonly ApplicationUser _testUser;

        public LoginRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var settings = fixture.Create<GlobalSettings>();
            _commandDispatcher = new Mock<ICommandDispatcher>();
            _queryDispatcher = new Mock<IQueryDispatcher>();
            _signInManager = GetMockSignInManager();
            _testLoginRequest = fixture.Create<LoginRequest>();
            _testUser = fixture.Create<ApplicationUser>();

            _queryDispatcher
                .Setup(x => x.Dispatch<UserGetByUsernameQuery, ApplicationUser>(It.IsNotNull<UserGetByUsernameQuery>()))
                .ReturnsAsync(_testUser);

            _signInManager
                .Setup(x => x.PasswordSignInAsync(It.IsNotNull<string>(), It.IsNotNull<string>(), true, false))
                .ReturnsAsync(SignInResult.Success);

            _handler = new LoginRequestHandler(_commandDispatcher.Object, _queryDispatcher.Object, settings, _signInManager.Object);
        }

        [Fact]
        public async Task Handler_Calls_Get_User_Query_When_Login_Succeeds()
        {
            await _handler.Handle(_testLoginRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<UserGetByUsernameQuery, ApplicationUser>(It.Is<UserGetByUsernameQuery>(q => q != null)), Times.Once);
        }

        [Fact]
        public async Task Handler_Calls_SignIn()
        {
            await _handler.Handle(_testLoginRequest, CancellationToken.None);

            _signInManager.Verify(x => x.PasswordSignInAsync(It.IsNotNull<string>(), It.IsNotNull<string>(), true, false), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_Unauthorized_When_Login_Fails()
        {
            _signInManager
                .Setup(x => x.PasswordSignInAsync(It.IsNotNull<string>(), It.IsNotNull<string>(), true, false))
                .ReturnsAsync(SignInResult.Failed);

            var result = await _handler.Handle(_testLoginRequest, CancellationToken.None);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Handler_Returns_User_Result_When_Login_Succeeds()
        {
            var result = await _handler.Handle(_testLoginRequest, CancellationToken.None) as OkObjectResult;

            result.Should().NotBeNull();
            var userResponse = (LoginResponse)result.Value;
            userResponse.DisplayName.Should().Be(_testUser.DisplayName);
            userResponse.Token.Should().NotBeNullOrWhiteSpace();
            userResponse.UserId.Should().Be(_testUser.Id);
            userResponse.UserNum.Should().Be(_testUser.UserNum);
        }

        [Fact]
        public async Task Handler_Saves_Refresh_Token()
        {
            await _handler.Handle(_testLoginRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.Is<RefreshTokenCreateCommand>(c => c != null)), Times.Once);
        }

        private Mock<SignInManager<ApplicationUser>> GetMockSignInManager()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUsrMgr = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new HttpContextAccessor();
            var mockUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var mockOptions = new Mock<IOptions<IdentityOptions>>();
            var mockLogger = new Mock<ILogger<SignInManager<ApplicationUser>>>();

            return new Mock<SignInManager<ApplicationUser>>(mockUsrMgr, contextAccessor, mockUserClaimsPrincipalFactory.Object, mockOptions.Object, mockLogger.Object, null, null);
        }
    }
}