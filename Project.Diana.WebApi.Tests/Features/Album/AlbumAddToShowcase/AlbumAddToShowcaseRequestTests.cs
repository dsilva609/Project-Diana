using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumAddToShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumAddToShowcase
{
    public class AlbumAddToShowcaseRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumAddToShowcaseRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_When_Album_Id_Is_Default()
        {
            Action createWithDefaultAlbumId = () => new AlbumAddToShowcaseRequest(0, _testUser);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_When_Album_Id_Is_Negative()
        {
            Action createWithNegativeAlbumId = () => new AlbumAddToShowcaseRequest(-1, _testUser);

            createWithNegativeAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_User_Id_Is_Missing(int albumId)
        {
            Action createWitMissingUserId = () => new AlbumAddToShowcaseRequest(albumId, new ApplicationUser { Id = string.Empty });

            createWitMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_User_Is_Null(int albumId)
        {
            Action createWithNullUser = () => new AlbumAddToShowcaseRequest(albumId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}