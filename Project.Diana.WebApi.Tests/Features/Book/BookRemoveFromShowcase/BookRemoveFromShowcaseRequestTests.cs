using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookRemoveFromShowcase
{
    public class BookRemoveFromShowcaseRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookRemoveFromShowcaseRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new BookRemoveFromShowcaseRequest(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new BookRemoveFromShowcaseRequest(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new BookRemoveFromShowcaseRequest(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int id)
        {
            Action createWithNullUser = () => new BookRemoveFromShowcaseRequest(id, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}