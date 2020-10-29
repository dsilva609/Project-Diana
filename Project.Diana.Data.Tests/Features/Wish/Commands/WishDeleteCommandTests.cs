using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Commands
{
    public class WishDeleteCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new WishDeleteCommand(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new WishDeleteCommand(-1, user);

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