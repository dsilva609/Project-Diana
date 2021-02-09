using System;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookById
{
    public class BookGetByIdRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookGetByIdRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new BookGetByIdRequest(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Negative()
        {
            Action createWithDefaultId = () => new BookGetByIdRequest(-1, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}