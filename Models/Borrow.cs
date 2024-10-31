using System;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Borrow
    {
        [Key]
        public int BorrowId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool Returned { get; set; } = false;
    }
}
