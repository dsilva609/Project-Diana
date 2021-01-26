using System;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Queries;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Queries
{
    public class AlbumStatsGetQueryTests
    {
        [Fact]
        public void Query_Can_Be_Created_With_UserId()
        {
            Action createWithUserId = () => new AlbumStatsGetQuery(1);

            createWithUserId.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Query_Can_Be_Created_Without_UserId()
        {
            Action createWithoutUserId = () => new AlbumStatsGetQuery();

            createWithoutUserId.Should().NotThrow<ArgumentException>();
        }
    }
}