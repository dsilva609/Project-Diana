using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumList
{
    public class AlbumListGetRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Item_Count_Is_Less_Than_Zero(ApplicationUser user)
        {
            Action createWithItemCountLessThanZero = () => new AlbumListGetRequest(-1, user);

            createWithItemCountLessThanZero.Should().Throw<ArgumentException>();
        }
    }
}