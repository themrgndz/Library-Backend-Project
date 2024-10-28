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

        // Navigasyon özelliği
        public virtual ICollection<Book> Books { get; set; } // Category ile ilişki
    }
}
