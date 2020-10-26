using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Project.Diana.Provider.Features.GoogleBooks;
using Project.Diana.WebApi.Features.Book.SearchGoogleBooks;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.SearchGoogleBooks
{
    public class SearchGoogleBooksRequestHandlerTests
    {
        private readonly Mock<IGoogleBooksProvider> _googleBooksProvider;
        private readonly SearchGoogleBooksRequestHandler _handler;
        private readonly SearchGoogleBooksRequest _testRequest;

        public SearchGoogleBooksRequestHandlerTests()
        {
            var fixture = new Fixture();

            _googleBooksProvider = new Mock<IGoogleBooksProvider>();
            _testRequest = fixture.Create<SearchGoogleBooksRequest>();

            _handler = new SearchGoogleBooksRequestHandler(_googleBooksProvider.Object);
        }

        [Fact]
        public async Task Handler_Calls_Google_Books_Provider()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _googleBooksProvider.Verify(x => x.Search(_testRequest.Author, _testRequest.Title), Times.Once);
        }

        [Fact]
        public async Task Handler_Throws_If_Provider_Returns_Failure()
        {
            _googleBooksProvider.Setup(x => x.Search(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Result.Failure<IEnumerable<BookSearchResponse>>("failure"));

            Func<Task> callForProviderFailure = async () => await _handler.Handle(_testRequest, CancellationToken.None);

            await callForProviderFailure.Should().ThrowAsync<Exception>();
        }
    }
}