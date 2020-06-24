using System;
using FluentAssertions;
using Project.Diana.WebApi.Features.Wish;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish
{
    public class WishGetByIDRequestTests
    {
        [Fact]
        public void RequestThrowsWhenIDIsDefault()
        {
            Action createWithDefaultId = () => new WishGetByIDRequest(0);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}