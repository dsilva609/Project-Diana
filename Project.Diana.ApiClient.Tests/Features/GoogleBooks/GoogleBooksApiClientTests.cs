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
        private readonly VolumesResource.GetRequest _testGetRequest;
        private readonly VolumesResource.ListRequest _testListRequest;
        private readonly Mock<VolumesResource> _volumesResource;

        public GoogleBooksApiClientTests()
        {
            var fixture = new Fixture();

            var clientService = fixture.Create<BooksService>();

            _testGetRequest = new VolumesResource.GetRequest(clientService, "test");
            _testListRequest = new VolumesResource.ListRequest(clientService);
            _volumesResource = new Mock<VolumesResource>(clientService);

            _volumesResource.Setup(x => x.Get(It.IsAny<string>())).Returns(_testGetRequest);
            _volumesResource.Setup(x => x.List()).Returns(_testListRequest);

            _apiClient = new GoogleBooksApiClient(_volumesResource.Object);
        }

        [Theory, AutoData]
        public async Task Client_Get_By_Id_Gets_Request(string id)
        {
            await _apiClient.GetById(id);

            _volumesResource.Verify(x => x.Get(id), Times.Once);
        }

        [Fact]
        public async Task Client_Get_By_Id_Returns_Failure_If_Get_Fails()
        {
            var result = await _apiClient.GetById(string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Client_Search_Contains_No_Plus_When_Only_Searching_By_Author_In_Query(string author)
        {
            await _apiClient.Search(author, string.Empty);

            var query = _testListRequest.Q;

            query.Should().NotEndWith("+");
        }

        [Theory, AutoData]
        public async Task Client_Search_Contains_No_Plus_When_Only_Searching_By_Title_In_Query(string title)
        {
            await _apiClient.Search(string.Empty, title);

            var query = _testListRequest.Q;

            query.Should().NotStartWith("+");
        }

        [Theory, AutoData]
        public async Task Client_Search_Gets_List_Request(string author, string title)
        {
            await _apiClient.Search(author, title);

            _volumesResource.Verify(x => x.List(), Times.Once);
        }

        [Fact]
        public async Task Client_Search_Returns_Failure_If_Search_Fails()
        {
            var result = await _apiClient.Search(string.Empty, string.Empty);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Client_Search_Separates_Search_Terms_With_Plus_Sign(string author, string title)
        {
            await _apiClient.Search(author, title);

            _testListRequest.Q.Should().Be($"{author}+{title}");
        }
    }
}