using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Commands
{
    public class AlbumIncrementPlayCountCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Album_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultAlbumId = () => new AlbumIncrementPlayCountCommand(0, user);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Album_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeAlbumId = () => new AlbumIncrementPlayCountCommand(-1, user);

            createWithNegativeAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int albumId)
        {
            Action createWithMissingUserId = () => new AlbumIncrementPlayCountCommand(albumId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Null(int albumId)
        {
            Action createWithNUllUser = () => new AlbumIncrementPlayCountCommand(albumId, null);

            createWithNUllUser.Should().Throw<ArgumentException>();
        }
    }
}