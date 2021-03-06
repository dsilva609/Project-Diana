﻿using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Sql.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public class ProjectDianaWriteContext : DbContext, IProjectDianaWriteContext
    {
        public DbSet<AlbumRecord> Albums { get; set; }
        public DbSet<BookRecord> Books { get; set; }
        public DbSet<RefreshTokenRecord> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<WishRecord> Wishes { get; set; }

        public ProjectDianaWriteContext(DbContextOptions<ProjectDianaWriteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WishRecordConfiguration).Assembly);
        }
    }
}