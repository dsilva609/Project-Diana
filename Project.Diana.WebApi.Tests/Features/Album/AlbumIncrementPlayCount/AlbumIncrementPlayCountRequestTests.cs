using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumIncrementPlayCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumIncrementPlayCount
{
    public class AlbumIncrementPlayCountRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Album_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultAlbumId = () => new AlbumIncrementPlayCountRequest(0, user);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Album_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeAlbumId = () => new AlbumIncrementPlayCountRequest(-1, user);

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