using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Project.Diana.ApiClient.Features.Discogs;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.Discogs
{
    public class DiscogsApiClientConfigurationTests
    {
        private readonly DiscogsApiClientConfiguration _configuration;
        private readonly DiscogsApiClientConfigurationValidator _validator;

        public DiscogsApiClientConfigurationTests()
        {
            var fixture = new Fixture();

            _configuration = fixture.Create<DiscogsApiClientConfiguration>();

            _validator = new DiscogsApiClientConfigurationValidator();
        }

        [Fact]
        public void Configuration_Throws_If_Base_Url_Is_Missing()
        {
            _configuration.BaseUrl = string.Empty;

            Action createWithMissingBaseUrl = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingBaseUrl.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Discogs_Token_Is_Missing()
        {
            _configuration.DiscogsToken = string.Empty;

            Action createWithMissingDiscogsToken = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingDiscogsToken.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Search_Resource_Is_Missing()
        {
            _configuration.SearchResource = string.Empty;

            Action createWithMissingSearchResource = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingSearchResource.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_User_Agent_Is_Missing()
        {
            _configuration.UserAgent = string.Empty;

            Action createWithMissingUserAgent = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingUserAgent.Should().Throw<ValidationException>();
        }
    }
}