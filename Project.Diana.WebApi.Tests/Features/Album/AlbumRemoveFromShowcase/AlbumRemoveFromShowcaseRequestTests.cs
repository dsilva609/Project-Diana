using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumRemoveFromShowcase
{
    public class AlbumRemoveFromShowcaseRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumRemoveFromShowcaseRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new AlbumRemoveFromShowcaseRequest(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new AlbumRemoveFromShowcaseRequest(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new AlbumRemoveFromShowcaseRequest(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int id)
        {
            Action createWithNullUser = () => new AlbumRemoveFromShowcaseRequest(id, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}