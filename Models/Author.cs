using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Biography length can't exceed 200 characters")]
        public string Biography { get; set; }

        [Range(1000, 9999, ErrorMessage = "Birth year should be a valid year")]
        public int? BirthYear { get; set; }

        [Range(1000, 9999, ErrorMessage = "Death year should be a valid year")]
        public int? DeathYear { get; set; }

        // Navigasyon özelliği
        public virtual ICollection<Book> Books { get; set; } // Author ile ilişki
    }
}
