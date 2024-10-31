using Microsoft.EntityFrameworkCore;
using UzmLibrary.Models;

namespace UzmLibrary.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrow> Borrows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Borrow>().ToTable("Borrows");

            // User ve Book ile Borrow arasındaki ilişkileri tanımlama
            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrows)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Cascade delete olmaması için

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrows)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);  // Cascade delete olmaması için
        }
    }
}
