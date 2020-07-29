using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.WebApi.Features.User;
using Project.Diana.WebApi.Features.User.Login;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.User.Login
{
    public class LoginRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Password_Is_Missing(string username)
        {
            Action createWithMissingPassword = () => new LoginRequest(username, string.Empty);

            createWithMissingPassword.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Username_Is_Missing(string password)
        {
            Action createWithMissingUsername = () => new LoginRequest(string.Empty, password);

            createWithMissingUsername.Should().Throw<ArgumentException>();
        }
    }
}