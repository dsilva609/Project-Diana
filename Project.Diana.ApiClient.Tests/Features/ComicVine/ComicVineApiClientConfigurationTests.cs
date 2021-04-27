using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Project.Diana.ApiClient.Features.ComicVine;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.ComicVine
{
    public class ComicVineApiClientConfigurationTests
    {
        private readonly ComicVineApiClientConfiguration _configuration;
        private readonly ComicVineApiClientConfigurationValidator _validator;

        public ComicVineApiClientConfigurationTests()
        {
            var fixture = new Fixture();

            _configuration = fixture.Create<ComicVineApiClientConfiguration>();

            _validator = new ComicVineApiClientConfigurationValidator();
        }

        [Fact]
        public void Configuration_Throws_If_Api_Key_Is_Missing()
        {
            _configuration.ApiKey = string.Empty;

            Action createWithMissingApiKey = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingApiKey.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Base_Url_Is_Missing()
        {
            _configuration.BaseUrl = string.Empty;

            Action createWithMissingBaseUrl = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingBaseUrl.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Format_Is_Missing()
        {
            _configuration.Format = string.Empty;

            Action createWithMissingFormat = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingFormat.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Issue_Resource_Is_Missing()
        {
            _configuration.IssueResource = string.Empty;

            Action createWithMissingIssueResource = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingIssueResource.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Configuration_Throws_If_Limit_Is_Less_Than_Or_Equal_To_Zero(int limit)
        {
            _configuration.Limit = limit;

            Action createWithInvalidLimit = () => _validator.ValidateAndThrow(_configuration);

            createWithInvalidLimit.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Resource_Type_Is_Missing()
        {
            _configuration.ResourceType = string.Empty;

            Action createWithMissingResourceType = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingResourceType.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Configuration_Throws_If_Search_Resource_Is_Missing()
        {
            _configuration.SearchResource = string.Empty;

            Action createWithMissingSearchResource = () => _validator.ValidateAndThrow(_configuration);

            createWithMissingSearchResource.Should().Throw<ValidationException>();
        }
    }
}