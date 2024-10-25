using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UzmLibrary.Models;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    // Retrieves all books, including their authors and categories
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.Include(b => b.Author).Include(b => b.Category).ToListAsync();
    }

    // Retrieves the book with the specified ID, including its author and category
    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Books.Include(b => b.Author).Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    // Adds a new book to the database
    public async Task AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    // Updates the existing book in the database
    public async Task UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    // Deletes the book with the specified ID from the database
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
