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
    }
}
