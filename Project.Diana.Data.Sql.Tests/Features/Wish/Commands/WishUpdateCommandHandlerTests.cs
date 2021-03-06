using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Commands
{
    public class WishUpdateCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IFixture _fixture;
        private readonly WishUpdateCommandHandler _handler;
        private readonly WishUpdateCommand _testCommand;
        private readonly WishRecord _testRecord;
        private readonly IProjectDianaWriteContext _writeContext;

        public WishUpdateCommandHandlerTests()
        {
            _fixture = new Fixture();

            _testRecord = _fixture.Create<WishRecord>();
            _testCommand = new WishUpdateCommand(
                "api",
                "category",
                "image",
                ItemReference.Album,
                "note",
                true,
            "title",
                _testRecord.UserId,
                _testRecord.Id);

            _writeContext = InitializeDatabase();

            _handler = new WishUpdateCommandHandler(_writeContext);
        }

        [Fact]
        public async Task Handler_Throws_If_Wish_Is_Not_Found()
        {
            var command = _fixture.Create<WishUpdateCommand>();

            Func<Task> callForNonExistingWish = async () => await _handler.Handle(command);

            await callForNonExistingWish.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Handler_Updates_Wish()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var updatedRecord = await _writeContext.Wishes.FirstOrDefaultAsync(w => w.Id == _testRecord.Id);

            updatedRecord.Title.Should().Be(_testCommand.Title);
            updatedRecord.DateModified.Should().Be(_testRecord.DateModified);
        }

        private async Task InitializeRecords()
        {
            await _writeContext.Wishes.AddAsync(_testRecord);

            await _writeContext.SaveChangesAsync();
        }
    }
}