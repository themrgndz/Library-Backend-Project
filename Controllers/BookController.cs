using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebVize.Data;
using WebVize.Models;

namespace WebVize.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly BookContext _context;

        public BookController(ILogger<BookController> logger, BookContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Kitapların listelenmesi
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.AsNoTracking().ToList();
            _logger.LogInformation("All books requested.");
            return Ok(books);
        }

        // ID'ye göre tek bir kitabın alınması
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id); // AsNoTracking kaldırıldı
            if (book == null)
                return NotFound();

            _logger.LogInformation($"Book with ID {id} requested.");
            return Ok(book);
        }

        // Yeni bir kitap ekleme
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Books book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("New book added.");
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // Mevcut bir kitabı güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Books updatedBook)
        {
            var book = await _context.Books.FindAsync(id); // AsNoTracking kaldırıldı
            if (book == null)
                return NotFound();

            // Güncellenen değerleri al
            _context.Entry(book).CurrentValues.SetValues(updatedBook);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Book with ID {id} updated.");
            return NoContent();
        }

        // Bir kitabı silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Book with ID {id} deleted.");
            return NoContent();
        }

        // Favorilere ekleme
        [HttpPost("favorites")]
        public async Task<IActionResult> AddToFavorites([FromBody] FavoriteRequest request)
        {
            // Kullanıcının favori kitaplar listesine ekleme işlemi yapılacak
            // Kullanıcının kimliğini almak için bir sistem eklenebilir

            var favorite = new Favorite 
            {
                BookId = request.BookId,
                // UserId = request.UserId, // Kullanıcı kimliğini buraya ekleyin
                AddedDate = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Book with ID {request.BookId} added to favorites.");
            return Ok(new { message = "Kitap favorilere eklendi." });
        }

        // Kitap ödünç alma
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRequest request)
        {
            // Kullanıcının ödünç aldığı kitaplar listesine ekleme işlemi yapılacak
            // Kullanıcının kimliğini almak için bir sistem eklenebilir

            var borrowedBook = new BorrowedBook
            {
                BookId = request.BookId,
                // UserId = request.UserId, // Kullanıcı kimliğini buraya ekleyin
                BorrowDate = DateTime.UtcNow,
                // ReturnDate = null // İade tarihi yok
            };

            _context.BorrowedBooks.Add(borrowedBook);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Book with ID {request.BookId} borrowed.");
            return Ok(new { message = "Kitap başarıyla ödünç alındı." });
        }
    }

    public class FavoriteRequest
    {
        public int BookId { get; set; }
        // public int UserId { get; set; } // Kullanıcı kimliği ekleyin
    }

    public class BorrowRequest
    {
        public int BookId { get; set; }
        // public int UserId { get; set; } // Kullanıcı kimliği ekleyin
    }

    // Favori ve Ödünç kitaplar için model tanımları yapılabilir.
}
