using System;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookGetLatestAddedQueryTests
    {
        [Fact]
        public void Query_Sets_Default_Item_Count_For_Zero()
        {
            var query = new BookGetLatestAddedQuery(0);

            query.ItemCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Query_Throws_When_Item_Count_Is_Negative()
        {
            Action createWithNegativeItemCount = () => new BookGetLatestAddedQuery(-1);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }
    }
}