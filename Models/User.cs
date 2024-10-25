using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username length can't exceed 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        // Kullanıcının sahip olduğu rezervasyonlar
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Kullanıcının rollerini tutan koleksiyon
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
