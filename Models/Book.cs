using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UzmLibrary.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title length can't exceed 200 characters")]
        public string Title { get; set; }

        // Foreign key for Author (nullable)
        public int? AuthorId { get; set; } // Nullable Foreign Key
        [ForeignKey(nameof(AuthorId))]
        public virtual Author Author { get; set; }

        // Foreign key for Publisher (nullable)
        public int? PublisherId { get; set; } // Nullable Foreign Key
        [ForeignKey(nameof(PublisherId))]
        public virtual Publisher Publisher { get; set; }

        [Range(1000, 9999, ErrorMessage = "Publication year should be a valid year")]
        public int PublicationYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page count should be a positive number")]
        public int PageCount { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be a 13-digit number")]
        public string ISBN { get; set; }

        // Foreign key for Category (nullable)
        public int? CategoryId { get; set; } // Nullable Foreign Key
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public string Language { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock should be a non-negative number")]
        public int Stock { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
        public string Description { get; set; }

        // Navigation property for Reservations
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
