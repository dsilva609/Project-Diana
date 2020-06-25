using System;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryTests
    {
        [Fact]
        public void QueryThrowsWhenUserIDIsDefault()
        {
            Action createWithDefaultUserID = () => new WishGetByIDQuery(0, 1);

            createWithDefaultUserID.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void QueryThrowsWhenWishIDIsDefault()
        {
            Action createWithDefaultWishID = () => new WishGetByIDQuery(1, 0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}