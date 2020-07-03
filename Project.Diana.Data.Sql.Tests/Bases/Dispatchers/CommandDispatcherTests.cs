using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Bases.Dispatchers
{
    public class CommandDispatcherTests
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public CommandDispatcherTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();

            _serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new TestCommandHandler());

            _commandDispatcher = new CommandDispatcher(_serviceProvider.Object);
        }

        [Fact]
        public async Task Dispatcher_Calls_Get_Service()
        {
            await _commandDispatcher.Dispatch(new TestCommand());

            _serviceProvider.Verify(x => x.GetService(It.Is<Type>(y => y == typeof(ICommandHandler<TestCommand>))), Times.Once);
        }

        [Fact]
        public async Task Dispatcher_Throws_If_Command_Is_Null()
        {
            Func<Task> callWithNullCommand = async () => await _commandDispatcher.Dispatch<TestCommand>(null);

            await callWithNullCommand.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Dispatcher_Throws_If_No_Handler_Is_Found()
        {
            _serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

            Func<Task> callForNoHandler = async () => await _commandDispatcher.Dispatch(new TestCommand());

            await callForNoHandler.Should().ThrowAsync<ArgumentException>();
        }
    }
}