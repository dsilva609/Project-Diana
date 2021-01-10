using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Album.AlbumGetLatestAdded;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumGetLatestAdded
{
    public class AlbumGetLatestAddedRequestTests
    {
        [Fact]
        public void Request_Throws_When_Item_Count_Is_Negative()
        {
            Action createWithNegativeItemCount = () => new AlbumGetLatestAddedRequest(-1);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }
    }
}