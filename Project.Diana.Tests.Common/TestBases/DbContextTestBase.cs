using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Project.Diana.Tests.Common.TestBases
{
    public class DbContextTestBase<TContext> where TContext : DbContext
    {
        public TContext InitializeDatabase()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TContext>().UseSqlite(connection).Options;

            using var context = (TContext)Activator.CreateInstance(typeof(TContext), options);
            context.Database.EnsureCreated();

            return (TContext)Activator.CreateInstance(typeof(TContext), options);
        }
    }
}