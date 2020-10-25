using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Commands
{
    public class BookIncrementReadCountCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Book_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultBookId = () => new BookIncrementReadCountCommand(0, user);

            createWithDefaultBookId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Book_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeBookId = () => new BookIncrementReadCountCommand(-1, user);

            createWithNegativeBookId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int bookId)
        {
            Action createWithMissingUserId = () => new BookIncrementReadCountCommand(bookId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Null(int bookId)
        {
            Action createWithNUllUser = () => new BookIncrementReadCountCommand(bookId, null);

            createWithNUllUser.Should().Throw<ArgumentException>();
        }
    }
}