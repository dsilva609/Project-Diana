using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookAddToShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookAddToShowcase
{
    public class BookAddToShowcaseRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookAddToShowcaseRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new BookAddToShowcaseRequest(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new BookAddToShowcaseRequest(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int bookId)
        {
            Action createWithMissingUserId = () => new BookAddToShowcaseRequest(bookId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int bookId)
        {
            Action createWithNullUser = () => new BookAddToShowcaseRequest(bookId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}