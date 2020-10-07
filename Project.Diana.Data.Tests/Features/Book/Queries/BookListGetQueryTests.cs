using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookListGetQueryTests
    {
        [Theory, AutoData]
        public void Query_Throws_If_Item_Count_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeItemCount = () => new BookListGetQuery(-1, 0, user);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_If_Page_Is_Negative(int itemCount, ApplicationUser user)
        {
            Action createWithNegativePage = () => new BookListGetQuery(itemCount, -1, user);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}