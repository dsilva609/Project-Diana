using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryTests
    {
        [Theory, AutoData]
        public void QueryThrowsWhenUserIDIsDefault(int wishID)
        {
            Action createWithDefaultUserID = () => new WishGetByIDQuery(0, wishID);

            createWithDefaultUserID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void QueryThrowsWhenWishIDIsDefault(int userID)
        {
            Action createWithDefaultWishID = () => new WishGetByIDQuery(userID, 0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}