using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Book.SearchGoogleBooks;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.SearchGoogleBooks
{
    public class SearchGoogleBooksRequestTests
    {
        [Fact]
        public void Request_Throws_If_Author_And_Title_Are_Missing()
        {
            Action createWithMissingAuthorAndTitle = () => new SearchGoogleBooksRequest(string.Empty, string.Empty);

            createWithMissingAuthorAndTitle.Should().Throw<ArgumentException>();
        }
    }
}