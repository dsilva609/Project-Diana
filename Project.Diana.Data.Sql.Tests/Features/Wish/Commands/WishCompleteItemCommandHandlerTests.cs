using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Commands
{
    public class WishCompleteItemCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly WishCompleteItemCommandHandler _handler;
        private readonly WishCompleteItemCommand _testCommand;
        private readonly WishRecord _testRecord;
        private readonly IProjectDianaWriteContext _writeContext;

        public WishCompleteItemCommandHandlerTests()
        {
            var fixture = new Fixture();

            _testRecord = fixture.Create<WishRecord>();
            _testCommand = new WishCompleteItemCommand(_testRecord.UserId, _testRecord.Id);

            _writeContext = InitializeDatabase();

            _handler = new WishCompleteItemCommandHandler(_writeContext);
        }

        [Fact]
        public async Task Handler_Throws_If_Unable_To_Find_Wish_For_Id()
        {
            await InitializeRecords();

            var command = new WishCompleteItemCommand(_testRecord.UserId, _testRecord.Id + 1);

            Func<Task> callWithNonMatchingWishId = async () => await _handler.Handle(command);

            await callWithNonMatchingWishId.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Handler_Throws_If_Unable_To_Find_Wish_For_UserId()
        {
            await InitializeRecords();

            var command = new WishCompleteItemCommand($"{_testRecord.UserId}1", _testRecord.Id);

            Func<Task> callWithNonMatchingWishId = async () => await _handler.Handle(command);

            await callWithNonMatchingWishId.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Handler_Updates_Wish()
        {
            _testRecord.Owned = false;

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var updatedRecord = await _writeContext.Wishes.FirstOrDefaultAsync(w => w.Id == _testRecord.Id && w.UserId == _testCommand.UserId);

            updatedRecord.Owned.Should().BeTrue();
        }

        private async Task InitializeRecords()
        {
            await _writeContext.Wishes.AddAsync(_testRecord);

            await _writeContext.SaveChangesAsync();
        }
    }
}