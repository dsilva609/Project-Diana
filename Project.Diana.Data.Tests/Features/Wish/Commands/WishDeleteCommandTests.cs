using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Commands
{
    public class WishDeleteCommandTests
    {
        private readonly ApplicationUser _testUser;

        public WishDeleteCommandTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new WishDeleteCommand(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new WishDeleteCommand(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new WishDeleteCommand(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Missing(int id)
        {
            Action createWithMissingUser = () => new WishDeleteCommand(id, null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }
    }
}