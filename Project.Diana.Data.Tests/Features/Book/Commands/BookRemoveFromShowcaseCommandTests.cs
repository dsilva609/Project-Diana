using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Commands
{
    public class BookRemoveFromShowcaseCommandTests
    {
        private readonly ApplicationUser _testUser;

        public BookRemoveFromShowcaseCommandTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new BookRemoveFromShowcaseCommand(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new BookRemoveFromShowcaseCommand(-1, _testUser);

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