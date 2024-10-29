using System.Collections.Generic;

namespace UzmLibrary.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class RoleDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
