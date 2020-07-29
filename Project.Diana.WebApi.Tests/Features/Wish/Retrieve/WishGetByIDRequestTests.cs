﻿using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Wish.Retrieve;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Retrieve
{
    public class WishGetByIDRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_When_User_Is_Missing(int wishID)
        {
            Action createWithMissingUser = () => new WishGetByIDRequest(null, wishID);

            createWithMissingUser.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_UserID_Is_Missing(int wishID)
        {
            Action createWithMissingUserID = () => new WishGetByIDRequest(new ApplicationUser { Id = string.Empty }, wishID);

            createWithMissingUserID.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_When_WishID_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new WishGetByIDRequest(user, 0);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}