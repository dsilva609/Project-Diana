using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Book.BookGetLatestAdded;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookGetLatestAdded
{
    public class BookGetLatestAddedRequestTests
    {
        [Fact]
        public void Request_Throws_When_Item_Count_Is_Negative()
        {
            Action createWithNegativeItemCount = () => new BookGetLatestAddedRequest(-1);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }
    }
}