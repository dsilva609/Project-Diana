using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Diana.Data.Features.Book;

namespace Project.Diana.Data.Sql.Features.Book
{
    public class BookRecordConfiguration : IEntityTypeConfiguration<BookRecord>
    {
        public void Configure(EntityTypeBuilder<BookRecord> builder)
            => builder
                .ToTable("Books")
                .HasKey(book => book.ID);
    }
}