using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVize.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Books Book { get; set; }

        // public int UserId { get; set; } // Kullanıcı kimliği ile ilişkilendirilebilir

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Kitabın iade tarihi (opsiyonel)
    }
}
