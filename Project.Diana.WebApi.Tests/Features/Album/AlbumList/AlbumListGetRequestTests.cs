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
        public void Request_Throws_If_Item_Count_Is_Less_Than_Zero(string searchQuery, ApplicationUser user)
        {
            Action createWithItemCountLessThanZero = () => new AlbumListGetRequest(-1, 0, searchQuery, user);

            createWithItemCountLessThanZero.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Page_Is_Negative(int itemCount, string searchQuery, ApplicationUser user)
        {
            Action createWithNegativePage = () => new AlbumListGetRequest(itemCount, -1, searchQuery, user);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}