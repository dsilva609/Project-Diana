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
        public void Query_Throws_When_UserID_Is_Missing(int wishID)
        {
            Action createWithDefaultUserID = () => new WishGetByIDQuery(string.Empty, wishID);

            createWithDefaultUserID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_When_WishID_Is_Default(string userID)
        {
            Action createWithDefaultWishID = () => new WishGetByIDQuery(userID, 0);

            createWithDefaultWishID.Should().Throw<ArgumentException>();
        }
    }
}