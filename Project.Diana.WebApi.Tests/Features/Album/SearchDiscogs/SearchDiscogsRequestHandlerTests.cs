using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Project.Diana.ApiClient.Features.Discogs;
using Project.Diana.Provider.Features.Discogs;
using Project.Diana.WebApi.Features.Album.SearchDiscogs;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.SearchDiscogs
{
    public class SearchDiscogsRequestHandlerTests
    {
        private readonly Mock<IDiscogsProvider> _discogsProvider;
        private readonly SearchDiscogsRequestHandler _handler;
        private readonly SearchDiscogsRequest _testRequest;

        public SearchDiscogsRequestHandlerTests()
        {
            var fixture = new Fixture();

            _discogsProvider = new Mock<IDiscogsProvider>();
            _testRequest = fixture.Create<SearchDiscogsRequest>();

            _handler = new SearchDiscogsRequestHandler(_discogsProvider.Object);
        }

        [Fact]
        public async Task Handler_Calls_Discogs_Provider()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _discogsProvider.Verify(x => x.SearchForAlbum(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Throws_If_Provider_Returns_Failure()
        {
            _discogsProvider.Setup(x => x.SearchForAlbum(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Failure<IEnumerable<SearchResult>>("failure"));

            Func<Task> callForProviderFailure = async () => await _handler.Handle(_testRequest, CancellationToken.None);

            await callForProviderFailure.Should().ThrowAsync<Exception>();
        }
    }
}