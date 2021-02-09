using System;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumById
{
    public class AlbumGetByIdRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumGetByIdRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new AlbumGetByIdRequest(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new AlbumGetByIdRequest(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }
    }
}