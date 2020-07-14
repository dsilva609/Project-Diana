using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Features.User.Queries;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.User.Queries
{
    public class UserGetByUsernameQueryHandlerTests
    {
        private readonly UserGetByUsernameQueryHandler _handler;
        private readonly UserGetByUsernameQuery _testQuery;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;

        public UserGetByUsernameQueryHandlerTests()
        {
            var fixture = new Fixture();

            _testQuery = fixture.Create<UserGetByUsernameQuery>();
            _userManager = GetMockUserManager();

            _userManager.Setup(x => x.FindByEmailAsync(It.IsNotNull<string>()))
                .ReturnsAsync(fixture.Create<ApplicationUser>());

            _handler = new UserGetByUsernameQueryHandler(_userManager.Object);
        }

        [Fact]
        public async Task Handler_Calls_UserManager()
        {
            await _handler.Handle(_testQuery);

            _userManager.Verify(x => x.FindByEmailAsync(It.Is<string>(y => !string.IsNullOrWhiteSpace(y))), Times.Once);
        }

        [Fact]
        public async Task Handler_Returns_User()
        {
            var result = await _handler.Handle(_testQuery);

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