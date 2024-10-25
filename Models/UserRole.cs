using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; } // Foreign Key

        [Required(ErrorMessage = "Role ID is required")]
        public int RoleID { get; set; } // Foreign Key

        // Navigasyon özellikleri
        public virtual User User { get; set; } // User ile ilişki
        public virtual Role Role { get; set; } // Role ile ilişki
    }
}
