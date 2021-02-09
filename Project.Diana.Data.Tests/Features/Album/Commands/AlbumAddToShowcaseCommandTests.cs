using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Commands
{
    public class AlbumAddToShowcaseCommandTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumAddToShowcaseCommandTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Command_Throws_If_Album_Id_Is_Default()
        {
            Action createWithDefaultAlbumId = () => new AlbumAddToShowcaseCommand(0, _testUser);

            createWithDefaultAlbumId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_Album_Id_Is_Negative()
        {
            Action createWithNegativeAlbumId = () => new AlbumAddToShowcaseCommand(-1, _testUser);

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