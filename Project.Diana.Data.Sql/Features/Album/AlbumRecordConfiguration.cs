using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Diana.Data.Features.Album;

namespace Project.Diana.Data.Sql.Features.Album
{
    public class AlbumRecordConfiguration : IEntityTypeConfiguration<AlbumRecord>
    {
        public void Configure(EntityTypeBuilder<AlbumRecord> builder)
            => builder
                .ToTable("Albums")
                .HasKey(album => album.ID);
    }
}