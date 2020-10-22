using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Project.Diana.Data.Features.Settings;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Settings
{
    public class GlobalSettingsTests
    {
        private readonly GlobalSettings _settings;
        private readonly GlobalSettingsValidator _validator;

        public GlobalSettingsTests()
        {
            var fixture = new Fixture();

            _settings = fixture.Create<GlobalSettings>();

            _validator = new GlobalSettingsValidator();
        }

        [Fact]
        public void Settings_Throws_If_Issuer_Is_Missing()
        {
            _settings.Issuer = string.Empty;

            Action createWithMissingIssuer = () => _validator.ValidateAndThrow(_settings);

            createWithMissingIssuer.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Settings_Throws_If_Jwt_Key_Is_Missing()
        {
            _settings.JwtKey = string.Empty;

            Action createWithMissingJwtKey = () => _validator.ValidateAndThrow(_settings);

            createWithMissingJwtKey.Should().Throw<ValidationException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Settings_Throws_If_Token_Expiration_Is_Less_Than_Or_Equal_To_Zero(int expiration)
        {
            _settings.TokenExpirationMinutes = expiration;

            Action createWithInvalidExpiration = () => _validator.ValidateAndThrow(_settings);

            createWithInvalidExpiration.Should().Throw<ValidationException>();
        }
    }
}