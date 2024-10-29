using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UzmLibrary.Models;

namespace UzmLibrary.Services
{
    public class BookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTO>> GetBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Name,
                    PublicationYear = b.PublicationYear,
                    PageCount = b.PageCount,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    Stock = b.Stock,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description
                })
                .ToListAsync();
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Where(b => b.BookId == id)
                .Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Name,
                    PublicationYear = b.PublicationYear,
                    PageCount = b.PageCount,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    Stock = b.Stock,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<BookDTO>> SearchBooksAsync(string title)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .Where(b => b.Title.Contains(title))
                .Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    PublisherName = b.Publisher.Name,
                    CategoryName = b.Category.Name,
                    PublicationYear = b.PublicationYear,
                    PageCount = b.PageCount,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    Stock = b.Stock,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description
                })
                .ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
