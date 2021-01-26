using System;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookStatsGetQueryTests
    {
        [Fact]
        public void Query_Can_Be_Created_With_UserId()
        {
            Action createWithUserId = () => new BookStatsGetQuery(1);

            createWithUserId.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Query_Can_Be_Created_Without_UserId()
        {
            Action createWithoutUserId = () => new BookStatsGetQuery();

            createWithoutUserId.Should().NotThrow<ArgumentException>();
        }
    }
}