using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string Description { get; set; }

        // Navigasyon özelliği
        public virtual ICollection<Book> Books { get; set; } // Category ile ilişki
    }
}
