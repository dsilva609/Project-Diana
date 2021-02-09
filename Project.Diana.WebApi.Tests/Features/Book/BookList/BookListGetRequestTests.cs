using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookList
{
    public class BookListGetRequestTests
    {
        private readonly ApplicationUser _testUser;

        public BookListGetRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Request_Guards_Against_Negative_Item_Counts(string searchQuery)
        {
            Action createWithNegativeItemCount = () => new BookListGetRequest(-1, 0, searchQuery, _testUser);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Guards_Against_Negative_Page(int itemCount, string searchQuery)
        {
            Action createWithNegativePage = () => new BookListGetRequest(itemCount, -1, searchQuery, _testUser);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}