using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumIncrementPlayCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumIncrementPlayCount
{
    public class AlbumIncrementPlayCountRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumIncrementPlayCountRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Request_Throws_If_Album_Id_Is_Default()
        {
            Action createWithDefaultAlbumId = () => new AlbumIncrementPlayCountRequest(0, _testUser);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_Album_Id_Is_Negative()
        {
            Action createWithNegativeAlbumId = () => new AlbumIncrementPlayCountRequest(-1, _testUser);

            createWithNegativeAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int albumId)
        {
            Action createWithMissingUserId = () => new AlbumIncrementPlayCountRequest(albumId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int albumId)
        {
            Action createWithNullUser = () => new AlbumIncrementPlayCountRequest(albumId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}