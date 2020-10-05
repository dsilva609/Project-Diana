using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.User;
using Xunit;

namespace Project.Diana.Data.Tests.Features.Book.Queries
{
    public class BookGetByIdQueryTests
    {
        [Theory, AutoData]
        public void Query_Throws_If_Id_Is_Default(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookGetByIdQuery(0, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void Query_Throws_If_Id_Is_Negative(ApplicationUser user)
        {
            Action createWithDefaultId = () => new BookGetByIdQuery(-1, user);

            createWithDefaultId.Should().Throw<ArgumentException>();
        }
    }
}