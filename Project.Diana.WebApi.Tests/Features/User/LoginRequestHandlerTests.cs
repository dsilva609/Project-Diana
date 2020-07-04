using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Project.Diana.Data.Features.Settings;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.User;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.User
{
    public class LoginRequestHandlerTests
    {
        private readonly LoginRequestHandler _handler;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
        private readonly LoginRequest _testLoginRequest;

        public LoginRequestHandlerTests()
        {
            var fixture = new Fixture();

            var settings = fixture.Create<GlobalSettings>();
            _signInManager = GetMockSignInManager();
            _testLoginRequest = fixture.Create<LoginRequest>();

            _signInManager
                .Setup(x => x.PasswordSignInAsync(It.IsNotNull<string>(), It.IsNotNull<string>(), true, false))
                .ReturnsAsync(SignInResult.Success);

            _handler = new LoginRequestHandler(settings, _signInManager.Object);
        }

        [Fact]
        public async Task Handler_Calls_SignIn()
        {
            await _handler.Handle(_testLoginRequest, CancellationToken.None);

            _signInManager.Verify(x => x.PasswordSignInAsync(It.IsNotNull<string>(), It.IsNotNull<string>(), true, false), Times.Once);
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