using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumRemoveFromShowcase
{
    public class AlbumRemoveFromShowcaseRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new AlbumRemoveFromShowcaseRequest(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new AlbumRemoveFromShowcaseRequest(-1, user);

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