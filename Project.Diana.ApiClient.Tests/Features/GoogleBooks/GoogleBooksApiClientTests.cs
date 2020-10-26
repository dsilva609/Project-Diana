using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Google.Apis.Books.v1;
using Moq;
using Project.Diana.ApiClient.Features.GoogleBooks;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.GoogleBooks
{
    public class GoogleBooksApiClientTests
    {
        private readonly IGoogleBooksApiClient _apiClient;
        private readonly VolumesResource.ListRequest _testRequest;
        private readonly Mock<VolumesResource> _volumesResource;

        public GoogleBooksApiClientTests()
        {
            var fixture = new Fixture();

            var clientService = fixture.Create<BooksService>();

            _testRequest = new VolumesResource.ListRequest(clientService);
            _volumesResource = new Mock<VolumesResource>(clientService);

            _volumesResource.Setup(x => x.List()).Returns(_testRequest);

            _apiClient = new GoogleBooksApiClient(_volumesResource.Object);
        }

        [Theory, AutoData]
        public async Task Client_Gets_List_Request(string author, string title)
        {
            await _apiClient.Search(author, title);

            _volumesResource.Verify(x => x.List(), Times.Once);
        }

        [Theory, AutoData]
        public async Task Client_Returns_Book_Results(string author, string title)
        {
            var result = await _apiClient.Search(author, title);

            result.IsSuccess.Should().BeTrue();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Client_Returns_Failure_If_Search_Fails()
        {
            var result = await _apiClient.Search(string.Empty, string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Client_Sets_Author_In_Query(string author, string title)
        {
            await _apiClient.Search(author, title);

            var query = _testRequest.Q;

            query.Should().Contain($"inauthor:{author}");
        }

        [Theory, AutoData]
        public async Task Client_Sets_Title_In_Query(string author, string title)
        {
            await _apiClient.Search(author, title);

            var query = _testRequest.Q;

            query.Should().Contain($"intitle:{title}");
        }
    }
}