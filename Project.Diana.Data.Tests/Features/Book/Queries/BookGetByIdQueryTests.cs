using System;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookGetByIdQueryTests
    {
        private readonly ApplicationUser _testUser;

        public BookGetByIdQueryTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Query_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new BookGetByIdQuery(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Query_Throws_If_Id_Is_Negative()
        {
            Action createWithDefaultId = () => new BookGetByIdQuery(-1, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}