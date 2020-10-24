using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Album.SearchDiscogs;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.SearchDiscogs
{
    public class SearchDiscogsRequestTests
    {
        [Fact]
        public void Request_Throws_If_Both_Album_And_Artist_Are_Missing()
        {
            Action createWithMissingAlbumAndArtist = () => new SearchDiscogsRequest(string.Empty, string.Empty);

            createWithMissingAlbumAndArtist.Should().Throw<ArgumentException>();
        }
    }
}