using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.ApiClient.Features.Discogs;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.Discogs
{
    public class DiscogsApiClientConfigurationTests
    {
        [Theory, AutoData]
        public void Configuration_Throws_If_Base_Url_Is_Missing(string discogsToken, string userAgent)
        {
            Action createWithMissingBaseUrl = () => new DiscogsApiClientConfiguration(string.Empty, discogsToken, userAgent);

            createWithMissingBaseUrl.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Configuration_Throws_If_Discogs_Token_Is_Missing(string baseUrl, string userAgent)
        {
            Action createWithMissingDiscogsToken = () => new DiscogsApiClientConfiguration(baseUrl, string.Empty, userAgent);

            createWithMissingDiscogsToken.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Configuration_Throws_If_User_Agent_Is_Missing(string baseUrl, string discogsToken)
        {
            Action createWithMissingUserAgent = () => new DiscogsApiClientConfiguration(baseUrl, discogsToken, string.Empty);

            createWithMissingUserAgent.Should().Throw<ArgumentException>();
        }
    }
}