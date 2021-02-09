using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookIncrementReadCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookIncrementReadCount
{
    public class BookIncrementReadCountRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookIncrementReadCountRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Book_Id_Is_Default()
        {
            Action createWithDefaultBookId = () => new BookIncrementReadCountRequest(0, _testUser);

            createWithDefaultBookId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Book_Id_Is_Negative()
        {
            Action createWithNegativeBookId = () => new BookIncrementReadCountRequest(-1, _testUser);

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