using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookRemoveFromShowcase
{
    public class BookRemoveFromShowcaseRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookRemoveFromShowcaseRequest(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new BookRemoveFromShowcaseRequest(-1, user);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int id)
        {
            Action createWithMissingUserId = () => new BookRemoveFromShowcaseRequest(id, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int id)
        {
            Action createWithNullUser = () => new BookRemoveFromShowcaseRequest(id, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}