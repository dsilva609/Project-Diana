using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Commands
{
    public class BookRemoveFromShowcaseCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookRemoveFromShowcaseCommand(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new BookRemoveFromShowcaseCommand(-1, user);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new BookRemoveFromShowcaseCommand(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Null(int id)
        {
            Action createWithNullUser = () => new BookRemoveFromShowcaseCommand(id, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}