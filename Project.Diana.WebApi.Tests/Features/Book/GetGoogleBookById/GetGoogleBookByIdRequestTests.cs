using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Book.GetGoogleBookById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.GetGoogleBookById
{
    public class GetGoogleBookByIdRequestTests
    {
        [Fact]
        public void Request_Throws_If_Id_Is_Missing()
        {
            Action createWithMissingVolumeId = () => new GetGoogleBookByIdRequest(string.Empty);

            createWithMissingVolumeId.Should().Throw<ArgumentException>();
        }
    }
}