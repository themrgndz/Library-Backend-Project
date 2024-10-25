using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Address length can't exceed 200 characters")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "Contact email length can't exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ContactEmail { get; set; }

        [RegularExpression(@"^\+\d{1,3} \d{4,14}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        // Navigasyon özelliği
        public virtual ICollection<Book> Books { get; set; } // Publisher ile ilişki
    }
}
