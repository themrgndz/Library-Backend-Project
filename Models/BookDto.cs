using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class BookDto
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title length can't exceed 200 characters")]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        [Range(1000, 9999, ErrorMessage = "Publication year should be a valid year")]
        public int PublicationYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page count should be a positive number")]
        public int PageCount { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be a 13-digit number")]
        public string ISBN { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock should be a non-negative number")]
        public int Stock { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
        public string Description { get; set; }
    }
}
