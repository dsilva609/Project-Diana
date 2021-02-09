using System;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Queries
{
    public class AlbumGetByIdQueryTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumGetByIdQueryTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Query_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new AlbumGetByIdQuery(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Query_Throws_If_Id_Is_Negative()
        {
            Action createWithDefaultId = () => new AlbumGetByIdQuery(-1, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}