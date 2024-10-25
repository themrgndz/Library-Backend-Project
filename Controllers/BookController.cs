using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UzmLibrary.Models;
using UzmLibrary.Services;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: api/book
    // Retrieves all books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    // GET: api/book/{id}
    // Retrieves the book with the specified ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    // POST: api/book
    // Creates a new book
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        if (ModelState.IsValid)
        {
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }
        return BadRequest(ModelState);
    }

    // PUT: api/book/{id}
    // Updates the book with the specified ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.BookId || !ModelState.IsValid)
        {
            return BadRequest();
        }

        await _bookService.UpdateBookAsync(book);
        return NoContent();
    }

    // DELETE: api/book/{id}
    // Deletes the book with the specified ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}
