using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UzmLibrary.Models;
using UzmLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace UzmLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/borrow
        [HttpGet]
        public ActionResult<IEnumerable<Borrow>> GetBorrows()
        {
            return Ok(_context.Borrows.Include(b => b.User).Include(b => b.Book).ToList());
        }

        // GET: api/borrow/{id}
        [HttpGet("{id}")]
        public ActionResult<Borrow> GetBorrow(int id)
        {
            var borrow = _context.Borrows.Include(b => b.User).Include(b => b.Book).FirstOrDefault(b => b.BorrowId == id);
            if (borrow == null)
            {
                return NotFound();
            }
            return Ok(borrow);
        }

        // GET: api/borrow/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Borrow>> GetBorrowsByUserId(int userId)
        {
            var borrows = _context.Borrows
                .Include(b => b.User) 
                .Include(b => b.Book) 
                .Where(b => b.UserId == userId)
                .ToList();

            if (!borrows.Any())
            {
                return NotFound("No borrows found for this user.");
            }

            return Ok(borrows);
        }


        // POST: api/borrow
        [HttpPost]
        public async Task<ActionResult<Borrow>> PostBorrow(Borrow borrow)
        {
            if (borrow == null)
            {
                return BadRequest("Borrow data is null");
            }

            // Stok sayısını azalt
            var book = await _context.Books.FindAsync(borrow.BookId);
            if (book == null || book.Stock <= 0)
            {
                return BadRequest("Book is not available for borrowing.");
            }

            book.Stock--;
            _context.Books.Update(book);
            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrow), new { id = borrow.BorrowId }, borrow);
        }

        // PUT: api/borrow/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrow(int id, Borrow borrow)
        {
            if (id != borrow.BorrowId)
            {
                return BadRequest();
            }

            _context.Entry(borrow).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/borrow/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrow(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }

            // Stok sayısını artır
            var book = await _context.Books.FindAsync(borrow.BookId);
            if (book != null)
            {
                book.Stock++;
                _context.Books.Update(book);
            }

            _context.Borrows.Remove(borrow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowExists(int id)
        {
            return _context.Borrows.Any(e => e.BorrowId == id);
        }
    }
}
