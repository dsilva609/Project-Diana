using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookListGetQueryTests
    {
        private readonly ApplicationUser _testUser;

        public BookListGetQueryTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Query_Throws_If_Item_Count_Is_Negative(string searchQuery)
        {
            Action createWithNegativeItemCount = () => new BookListGetQuery(-1, 0, searchQuery, _testUser);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_If_Page_Is_Negative(int itemCount, string searchQuery)
        {
            Action createWithNegativePage = () => new BookListGetQuery(itemCount, -1, searchQuery, _testUser);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}