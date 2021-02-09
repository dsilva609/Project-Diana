using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Helpers;
using Xunit;

namespace Project.Diana.WebApi.Tests.Helpers
{
    public class CurrentUserServiceTests
    {
        private readonly ICurrentUserService _service;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;

        public CurrentUserServiceTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            _userManager = GetMockUserManager();

            var context = new DefaultHttpContext
            {
                User = fixture.Create<ClaimsPrincipal>()
            };

            httpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            _userManager.Setup(x => x.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(fixture.Create<ApplicationUser>());

            _service = new CurrentUserService(httpContextAccessor.Object, _userManager.Object);
        }

        [Fact]
        public async Task Service_Calls_UserManager()
        {
            await _service.GetCurrentUser();

            _userManager.Verify(x => x.GetUserAsync(It.IsNotNull<ClaimsPrincipal>()), Times.Once);
        }

        [Fact]
        public async Task Service_Returns_User()
        {
            var result = await _service.GetCurrentUser();

            result.Should().NotBeNull();
        }

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            return mockUserManager;
        }
    }
}