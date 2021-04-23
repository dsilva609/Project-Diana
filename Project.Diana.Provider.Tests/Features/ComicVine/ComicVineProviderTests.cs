using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Project.Diana.ApiClient.Features.ComicVine;
using Project.Diana.Provider.Features.ComicVine;
using Xunit;

namespace Project.Diana.Provider.Tests.Features.ComicVine
{
    public class ComicVineProviderTests
    {
        private readonly Mock<IComicVineApiClient> _apiClient;
        private readonly IComicVineProvider _provider;

        public ComicVineProviderTests()
        {
            _apiClient = new Mock<IComicVineApiClient>();

            _provider = new ComicVineProvider(_apiClient.Object);
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Empty_List_If_Client_Returns_No_Results(string title)
        {
            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>()))
                .ReturnsAsync(Result.Success(new ComicVineSearchResult { results = new List<ComicResult>() }));

            var result = await _provider.SearchForComic(title);

            result.Value.Should().BeEmpty();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Failure_If_Client_Returns_Failure(string title)
        {
            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>()))
                .ReturnsAsync(Result.Failure<ComicVineSearchResult>("failure"));

            var result = await _provider.SearchForComic(title);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Provider_Returns_Failure_If_Title_Is_Missing()
        {
            var result = await _provider.SearchForComic(string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Successful_Result(string title)
        {
            var fixture = new Fixture();

            var searchResult = fixture.Create<ComicVineSearchResult>();

            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>()))
                .ReturnsAsync(Result.Success(searchResult));

            var result = await _provider.SearchForComic(title);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }

        [Theory, AutoData]
        public async Task Provider_Sends_Search_Request(string title)
        {
            var fixture = new Fixture();

            var searchResult = fixture.Create<ComicVineSearchResult>();

            _apiClient.Setup(x => x.SendSearchRequest(It.IsAny<string>()))
                .ReturnsAsync(Result.Success(searchResult));

            await _provider.SearchForComic(title);

            _apiClient.Verify(x => x.SendSearchRequest(title), Times.Once);
        }
    }
}