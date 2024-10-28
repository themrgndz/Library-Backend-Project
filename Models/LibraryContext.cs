using Microsoft.EntityFrameworkCore;

namespace UzmLibrary.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        // DbSet tanımları
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        // Model yapılandırması
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Author ile Book arasındaki ilişki
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            // Publisher ile Book arasındaki ilişki
            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId);

            // Category ile Book arasındaki ilişki
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId);

            // User ile Reservation arasındaki ilişki
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            // Book ile Reservation arasındaki ilişki
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reservations)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookID);

            // Role ile UserRole arasındaki ilişki
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleID);

            // User ile UserRole arasındaki ilişki
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserID);
        }
    }
}
