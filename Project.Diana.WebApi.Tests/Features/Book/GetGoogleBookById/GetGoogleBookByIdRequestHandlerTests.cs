using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Project.Diana.Provider.Features.GoogleBooks;
using Project.Diana.WebApi.Features.Book.GetGoogleBookById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.GetGoogleBookById
{
    public class GetGoogleBookByIdRequestHandlerTests
    {
        private readonly Mock<IGoogleBooksProvider> _googleBooksProvider;
        private readonly GetGoogleBookByIdRequestHandler _handler;
        private readonly GetGoogleBookByIdRequest _testRequest;

        public GetGoogleBookByIdRequestHandlerTests()
        {
            var fixture = new Fixture();

            _googleBooksProvider = new Mock<IGoogleBooksProvider>();
            _testRequest = fixture.Create<GetGoogleBookByIdRequest>();

            _handler = new GetGoogleBookByIdRequestHandler(_googleBooksProvider.Object);
        }

        [Fact]
        public async Task Handler_Calls_Google_Books_Provider()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _googleBooksProvider.Verify(x => x.GetVolumeById(_testRequest.Id), Times.Once);
        }

        [Fact]
        public async Task Handler_Throws_If_Provider_Returns_Failure()
        {
            _googleBooksProvider.Setup(x => x.GetVolumeById(It.IsAny<string>())).ReturnsAsync(Result.Failure<BookSearchResponse>("failure"));

            Func<Task> callForProviderFailure = async () => await _handler.Handle(_testRequest, CancellationToken.None);

            await callForProviderFailure.Should().ThrowAsync<Exception>();
        }
    }
}