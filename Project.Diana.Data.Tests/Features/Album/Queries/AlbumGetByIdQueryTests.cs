using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Queries
{
    public class AlbumGetByIdQueryTests
    {
        [Theory, AutoData]
        public void Query_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new AlbumGetByIdQuery(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithDefaultId = () => new AlbumGetByIdQuery(-1, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}