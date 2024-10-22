using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVize.Models
{
    public class FavoriteBooks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Books Book { get; set; }

        // public int UserId { get; set; } // Kullanıcı kimliği ile ilişkilendirilebilir

        public DateTime AddedDate { get; set; }
    }
}
