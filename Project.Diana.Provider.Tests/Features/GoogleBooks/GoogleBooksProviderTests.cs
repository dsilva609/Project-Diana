using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Google.Apis.Books.v1.Data;
using Moq;
using Project.Diana.ApiClient.Features.GoogleBooks;
using Project.Diana.Provider.Features.GoogleBooks;
using Xunit;

namespace Project.Diana.Provider.Tests.Features.GoogleBooks
{
    public class GoogleBooksProviderTests
    {
        private readonly Mock<IGoogleBooksApiClient> _apiClient;
        private readonly IGoogleBooksProvider _provider;

        public GoogleBooksProviderTests()
        {
            _apiClient = new Mock<IGoogleBooksApiClient>();

            _provider = new GoogleBooksProvider(_apiClient.Object);
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Empty_List_When_Api_Client_Returns_No_Results(string author, string title)
        {
            _apiClient.Setup(x => x.Search(author, title)).ReturnsAsync(Result.Success(new Volumes
            {
                Items = new List<Volume>()
            }));

            var result = await _provider.Search(author, title);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Failure_If_Api_Client_Returns_Failure(string author, string title)
        {
            _apiClient.Setup(x => x.Search(It.IsNotNull<string>(), It.IsNotNull<string>())).ReturnsAsync(Result.Failure<Volumes>("failure"));

            var result = await _provider.Search(author, title);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Provider_Returns_Failure_If_Author_And_Title_Are_Missing()
        {
            var result = await _provider.Search(string.Empty, string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Provider_Returns_Search_Results(string author, string title)
        {
            var fixture = new Fixture();

            var volumeInfo = new Volume.VolumeInfoData
            {
                PublishedDate = DateTime.UtcNow.ToString("s")
            };

            var volume = fixture
                .Build<Volume>()
                .With(v => v.VolumeInfo, volumeInfo)
                .Create();

            var volumes = fixture
                .Build<Volumes>()
                .With(v => v.Items, new List<Volume> { volume })
                .Create();

            _apiClient.Setup(x => x.Search(author, title)).ReturnsAsync(Result.Success(volumes));

            var result = await _provider.Search(author, title);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }

        [Theory, AutoData]
        public async Task Provider_Sets_Image_Urls_To_Https(string author, string title)
        {
            var fixture = new Fixture();

            var volumeInfo = new Volume.VolumeInfoData
            {
                ImageLinks = new Volume.VolumeInfoData.ImageLinksData
                {
                    Medium = "http://image.com"
                }
            };

            var volume = fixture
                .Build<Volume>()
                .With(v => v.VolumeInfo, volumeInfo)
                .Create();

            var volumes = fixture
                .Build<Volumes>()
                .With(v => v.Items, new List<Volume> { volume })
                .Create();

            _apiClient.Setup(x => x.Search(author, title)).ReturnsAsync(Result.Success(volumes));

            var result = await _provider.Search(author, title);

            result.IsSuccess.Should().BeTrue();
            result.Value.All(x => x.ImageUrl.StartsWith("https")).Should().BeTrue();
        }
    }
}