using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Context
{
    public interface IProjectDianaReadonlyContext
    {
        IQueryable<AlbumRecord> Albums { get; }
        IQueryable<BookRecord> Books { get; }
        IQueryable<RefreshTokenRecord> RefreshTokens { get; }
        IQueryable<ApplicationUser> Users { get; }
        IQueryable<WishRecord> Wishes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}