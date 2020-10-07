﻿using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Queries
{
    public class AlbumListGetQueryTests
    {
        [Theory, AutoData]
        public void Query_Sets_Default_Item_Count_For_Zero(ApplicationUser user)
        {
            var query = new AlbumListGetQuery(0, 1, user);

            query.ItemCount.Should().BeGreaterThan(0);
        }

        [Theory, AutoData]
        public void Query_Throws_When_Item_Count_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeItemCount = () => new AlbumListGetQuery(-1, 1, user);

            createWithNegativeItemCount.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_When_Page_Is_Negative(int itemCount, ApplicationUser user)
        {
            Action createWithNegativePage = () => new AlbumListGetQuery(itemCount, -1, user);

            createWithNegativePage.Should().Throw<ArgumentException>();
        }
    }
}