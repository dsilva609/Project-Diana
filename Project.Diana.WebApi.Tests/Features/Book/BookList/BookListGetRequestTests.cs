using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookList
{
    public class BookListGetRequestTests
    {
        [Theory, AutoData]
        public void Request_Guards_Against_Negative_Item_Counts(string searchQuery, ApplicationUser user)
        {
            Action createWithNegativeItemCount = () => new BookListGetRequest(-1, 0, searchQuery, user);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Guards_Against_Negative_Page(int itemCount, string searchQuery, ApplicationUser user)
        {
            Action createWithNegativePage = () => new BookListGetRequest(itemCount, -1, searchQuery, user);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}