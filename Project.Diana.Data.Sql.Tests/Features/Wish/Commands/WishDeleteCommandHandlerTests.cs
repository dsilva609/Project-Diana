using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Commands
{
    public class WishDeleteCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly WishDeleteCommandHandler _handler;
        private readonly WishDeleteCommand _testCommand;
        private readonly WishRecord _testRecord;

        public WishDeleteCommandHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = fixture.Create<WishDeleteCommand>();
            _testRecord = fixture
                .Build<WishRecord>()
                .With(w => w.Id, _testCommand.Id)
                .With(w => w.UserId, _testCommand.User.Id)
                .Create();

            _handler = new WishDeleteCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Delete_Wish_For_Different_User()
        {
            await InitializeRecords();

            var command = new WishDeleteCommand(_testCommand.Id, new ApplicationUser { Id = $"{_testCommand.User.Id}different user" });

            await _handler.Handle(command);

            var exists = await _context.Wishes.AnyAsync(w => w.Id == _testCommand.Id);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Does_Not_Remove_When_Not_Found_For_Id()
        {
            await InitializeRecords();

            var command = new WishDeleteCommand(_testCommand.Id + 1, _testCommand.User);

            await _handler.Handle(command);

            var exists = await _context.Wishes.AnyAsync(w => w.Id == _testCommand.Id);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Removes_Record()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var exists = await _context.Wishes.AnyAsync(w => w.Id == _testCommand.Id);

            exists.Should().BeFalse();
        }

        private async Task InitializeRecords()
        {
            await _context.Wishes.AddAsync(_testRecord);

            await _context.SaveChangesAsync();
        }
    }
}