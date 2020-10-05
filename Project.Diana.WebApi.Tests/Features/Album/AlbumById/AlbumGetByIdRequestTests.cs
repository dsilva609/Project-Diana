using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumById
{
    public class AlbumGetByIdRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new AlbumGetByIdRequest(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new AlbumGetByIdRequest(-1, user);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }
    }
}