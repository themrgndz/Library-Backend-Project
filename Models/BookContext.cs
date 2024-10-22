using Microsoft.EntityFrameworkCore;

namespace WebVize.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<FavoriteBooks> Favorites { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
    }
}
