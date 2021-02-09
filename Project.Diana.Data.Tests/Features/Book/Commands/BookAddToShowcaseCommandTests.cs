using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Commands
{
    public class BookAddToShowcaseCommandTests
    {
        private readonly ApplicationUser _testUser;

        public BookAddToShowcaseCommandTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Command_Throws_If_Book_Id_Is_Default()
        {
            Action createWithDefaultBookId = () => new BookAddToShowcaseCommand(0, _testUser);

            createWithDefaultBookId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_Book_Id_Is_Negative()
        {
            Action createWithNegativeBookId = () => new BookAddToShowcaseCommand(-1, _testUser);

            createWithNegativeBookId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int bookId)
        {
            Action createWithMissingUserId = () => new BookAddToShowcaseCommand(bookId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Null(int bookId)
        {
            Action createWithNullUser = () => new BookAddToShowcaseCommand(bookId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}