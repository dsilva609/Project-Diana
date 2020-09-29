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
        public void Requests_Guards_Against_Negative_Item_Counts(ApplicationUser user)
        {
            Action createWithNegativeItemCount = () => new BookListGetRequest(-1, user);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }
    }
}