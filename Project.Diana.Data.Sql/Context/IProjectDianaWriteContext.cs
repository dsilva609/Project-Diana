using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaWriteContext
    {
        DbSet<AlbumRecord> Albums { get; set; }
        DbSet<BookRecord> Books { get; set; }
        DbSet<RefreshTokenRecord> RefreshTokens { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<WishRecord> Wishes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}