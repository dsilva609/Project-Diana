using System;
using FluentAssertions;
using Project.Diana.Data.Features.Wish.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Wish.Queries
{
    public class WishGetListByUserIdQueryTests
    {
        [Fact]
        public void Query_Throws_If_UserId_Is_Missing()
        {
            Action createWithMissingUserId = () => new WishGetListByUserIdQuery(string.Empty);

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }
    }
}