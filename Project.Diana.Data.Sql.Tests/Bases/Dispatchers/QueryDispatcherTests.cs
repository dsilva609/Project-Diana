using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.Data.Sql.Bases.Queries;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Bases.Dispatchers
{
    public class QueryDispatcherTests
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public QueryDispatcherTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();

            _serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(new TestQueryHandler());

            _queryDispatcher = new QueryDispatcher(_serviceProvider.Object);
        }

        [Fact]
        public async Task Dispatcher_Calls_GetService()
        {
            await _queryDispatcher.Dispatch<TestQuery, string>(new TestQuery());

            _serviceProvider.Verify(x => x.GetService(It.Is<Type>(y => y == typeof(IQueryHandler<TestQuery, string>))), Times.Once);
        }

        [Fact]
        public async Task Dispatcher_Throws_If_No_Query_Handler_Is_Found()
        {
            _serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(null);

            Func<Task> callForNoHandler = async () => await _queryDispatcher.Dispatch<TestQuery, string>(new TestQuery());

            await callForNoHandler.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Dispatcher_Throws_If_Query_Is_Null()
        {
            Func<Task> callWithNullQuery = async () => await _queryDispatcher.Dispatch<TestQuery, string>(null);

            await callWithNullQuery.Should().ThrowAsync<ArgumentException>();
        }
    }
}