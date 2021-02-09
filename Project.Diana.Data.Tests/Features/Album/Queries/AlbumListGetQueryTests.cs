using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Queries
{
    public class AlbumListGetQueryTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumListGetQueryTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Theory, AutoData]
        public void Query_Sets_Default_Item_Count_For_Zero(string searchQuery)
        {
            var query = new AlbumListGetQuery(0, 1, searchQuery, _testUser);

            query.ItemCount.Should().BeGreaterThan(0);
        }

        [Theory, AutoData]
        public void Query_Throws_When_Item_Count_Is_Negative(string searchQuery)
        {
            Action createWithNegativeItemCount = () => new AlbumListGetQuery(-1, 1, searchQuery, _testUser);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_When_Page_Is_Negative(int itemCount, string searchQuery)
        {
            Action createWithNegativePage = () => new AlbumListGetQuery(itemCount, -1, searchQuery, _testUser);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}