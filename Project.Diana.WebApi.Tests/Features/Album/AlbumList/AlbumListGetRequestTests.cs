using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumList
{
    public class AlbumListGetRequestTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumListGetRequestTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Item_Count_Is_Less_Than_Zero(string searchQuery)
        {
            Action createWithItemCountLessThanZero = () => new AlbumListGetRequest(-1, 0, searchQuery, _testUser);

            createWithItemCountLessThanZero.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Page_Is_Negative(int itemCount, string searchQuery)
        {
            Action createWithNegativePage = () => new AlbumListGetRequest(itemCount, -1, searchQuery, _testUser);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}