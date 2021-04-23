using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Project.Diana.ApiClient.Features.Discogs;
using Project.Diana.Provider.Features.Discogs;
using Xunit;

namespace Project.Diana.Provider.Tests.Features.Discogs
{
    public class DiscogsProviderTests
    {
        private readonly Mock<IDiscogsApiClient> _apiClient;
        private readonly IDiscogsProvider _provider;

        public DiscogsProviderTests()
        {
            _apiClient = new Mock<IDiscogsApiClient>();

            _provider = new DiscogsProvider(_apiClient.Object);
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Empty_List_If_Client_Returns_No_Results(string album, string artist)
        {
            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(
                Result.Success(new DiscogsSearchResult
                {
                    results = new List<SearchResult>()
                }));

            var result = await _provider.SearchForAlbum(artist, album);

            result.Value.Should().BeEmpty();
        }

        [Fact]
        public async Task Provider_Returns_Failure_If_Artist_And_Album_Are_Null()
        {
            var result = await _provider.SearchForAlbum(string.Empty, string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Failure_If_Client_Returns_Failure(string album, string artist)
        {
            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Result.Failure<DiscogsSearchResult>("failure"));

            var result = await _provider.SearchForAlbum(artist, album);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Successful_Result(string album, string artist)
        {
            var fixture = new Fixture();

            var searchResult = fixture.Create<DiscogsSearchResult>();

            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Success(searchResult));

            var result = await _provider.SearchForAlbum(artist, album);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }

        [Theory, AutoData]
        public async Task Provider_Sends_Search_Request(string album, string artist)
        {
            var fixture = new Fixture();

            var searchResult = fixture.Create<DiscogsSearchResult>();

            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Success(searchResult));

            await _provider.SearchForAlbum(album, artist);

            _apiClient.Verify(x => x.SendSearchRequest(album, artist), Times.Once);
        }
    }
}