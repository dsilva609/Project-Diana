using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookAddToShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookAddToShowcase
{
    public class BookAddToShowcaseRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookAddToShowcaseRequest(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithNegativeId = () => new BookAddToShowcaseRequest(-1, user);

            createWithNegativeId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Id_Is_Missing(int bookId)
        {
            Action createWithMissingUserId = () => new BookAddToShowcaseRequest(bookId, new ApplicationUser { Id = string.Empty });

            createWithMissingUserId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_User_Is_Null(int bookId)
        {
            Action createWithNullUser = () => new BookAddToShowcaseRequest(bookId, null);

            createWithNullUser.Should().Throw<ArgumentException>();
        }
    }
}