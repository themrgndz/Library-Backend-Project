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

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
