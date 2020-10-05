using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.WebApi.Features.Book.BookById;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookById
{
    public class BookGetByIdRequestTests
    {
        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookGetByIdRequest(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Request_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookGetByIdRequest(-1, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}