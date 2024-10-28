using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [StringLength(50, ErrorMessage = "Role name length can't exceed 50 characters")]
        public string RoleName { get; set; }

        // Navigasyon özelliği
        public virtual ICollection<UserRole> UserRoles { get; set; } // UserRole ile ilişki
    }
}
