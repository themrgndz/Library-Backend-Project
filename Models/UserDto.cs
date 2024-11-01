using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username length can't exceed 50 characters")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
