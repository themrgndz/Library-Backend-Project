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

        public async Task AddBookAsync(BookDTO bookDto)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == bookDto.AuthorName);
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == bookDto.PublisherName);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == bookDto.CategoryName);

            // Author, Publisher ve Category yoksa oluştur
            if (author == null) {
                author = new Author { Name = bookDto.AuthorName };
                await _context.Authors.AddAsync(author);
            }
            if (publisher == null) {
                publisher = new Publisher { Name = bookDto.PublisherName };
                await _context.Publishers.AddAsync(publisher);
            }
            if (category == null) {
                category = new Category { Name = bookDto.CategoryName };
                await _context.Categories.AddAsync(category);
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = author,
                Publisher = publisher,
                Category = category,
                PublicationYear = bookDto.PublicationYear,
                PageCount = bookDto.PageCount,
                ISBN = bookDto.ISBN,
                Language = bookDto.Language,
                Stock = bookDto.Stock,
                ImageUrl = bookDto.ImageUrl,
                Description = bookDto.Description
            };

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
