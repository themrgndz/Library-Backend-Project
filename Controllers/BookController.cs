using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UzmLibrary.Models;
using UzmLibrary.Services;

namespace UzmLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks(string title)
        {
            var books = await _bookService.SearchBooksAsync(title);
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookDTO bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }

            // BookDTO'yu Book nesnesine dönüştür
            var book = new Book
            {
                Title = bookDto.Title,
                PublicationYear = bookDto.PublicationYear,
                PageCount = bookDto.PageCount,
                ISBN = bookDto.ISBN,
                Language = bookDto.Language,
                Stock = bookDto.Stock,
                ImageUrl = bookDto.ImageUrl,
                Description = bookDto.Description,
            };

            // İlişkili Author, Publisher ve Category'yi ayarla
            await _bookService.AddBookAsync(bookDto);

            // Yeni oluşturulan kitabın tüm ilişkileriyle birlikte verilerini getir
            var createdBook = await _bookService.GetBookByIdAsync(book.BookId);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
