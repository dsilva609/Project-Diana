using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Commands
{
    public class AlbumAddToShowcaseCommandTests
    {
        [Theory, AutoData]
        public void Command_Throws_If_Album_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultAlbumId = () => new AlbumAddToShowcaseCommand(0, user);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_Album_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeAlbumId = () => new AlbumAddToShowcaseCommand(-1, user);

            createWithNegativeAlbumId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int albumId)
        {
            Action createWithMissingUserId = () => new AlbumAddToShowcaseCommand(albumId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Missing(int albumId)
        {
            Action createWithMissingUser = () => new AlbumAddToShowcaseCommand(albumId, null);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }
    }
}