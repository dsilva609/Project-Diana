using System;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Queries
{
    public class WishGetListByUserIDQueryTests
    {
        [Fact]
        public void Query_Throws_If_UserID_Is_Missing()
        {
            Action createWithMissingUserID = () => new WishGetListByUserIDQuery(string.Empty);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }
    }
}