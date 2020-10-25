using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookIncrementReadCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookIncrementReadCount
{
    public class BookIncrementReadCountRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Book_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultBookId = () => new BookIncrementReadCountRequest(0, user);

            createWithDefaultBookId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Book_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeBookId = () => new BookIncrementReadCountRequest(-1, user);

            createWithNegativeBookId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int bookId)
        {
            Action createWithMissingUserId = () => new BookIncrementReadCountRequest(bookId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int bookId)
        {
            Action createWithNullUser = () => new BookIncrementReadCountRequest(bookId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}