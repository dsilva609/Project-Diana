﻿using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Album.Commands
{
    public class AlbumRemoveFromShowcaseCommandTests
    {
        private readonly ApplicationUser _testUser;

        public AlbumRemoveFromShowcaseCommandTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _testUser = fixture.Create<ApplicationUser>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Default()
        {
            Action createWithDefaultId = () => new AlbumRemoveFromShowcaseCommand(0, _testUser);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Command_Throws_If_Id_Is_Negative()
        {
            Action createWithNegativeId = () => new AlbumRemoveFromShowcaseCommand(-1, _testUser);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new AlbumRemoveFromShowcaseCommand(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Command_Throws_If_User_Is_Null(int id)
        {
            Action createWithNullUser = () => new AlbumRemoveFromShowcaseCommand(id, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}