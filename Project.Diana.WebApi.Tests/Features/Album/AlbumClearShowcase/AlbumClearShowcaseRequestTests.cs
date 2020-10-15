using System;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumClearShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumClearShowcase
{
    public class AlbumClearShowcaseRequestTests
    {
        [Fact]
        public void Request_Throws_If_User_Id_Is_Missing()
        {
            Action createWithMissingUserId = () => new AlbumClearShowcaseRequest(new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_Throws_If_User_Is_Null()
        {
            Action createWithNullUser = () => new AlbumClearShowcaseRequest(null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}