using System;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Commands
{
    public class AlbumClearShowcaseCommandTests
    {
        [Fact]
        public void Command_Throws_If_User_Id_Is_Missing()
        {
            Action createWithMissingUserId = () => new AlbumClearShowcaseCommand(new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_User_Is_Null()
        {
            Action createWithNullUser = () => new AlbumClearShowcaseCommand(null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}