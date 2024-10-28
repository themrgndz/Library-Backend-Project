using System;
using System.ComponentModel.DataAnnotations;

namespace UzmLibrary.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; } // Foreign Key

        [Required(ErrorMessage = "Book ID is required")]
        public int BookID { get; set; } // Foreign Key

        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Returned info is required")]
        public bool Returned { get; set; }

        public DateTime? ReturnDate { get; set; }

        // Navigasyon özellikleri
        public virtual User User { get; set; } // User ile ilişki
        public virtual Book Book { get; set; } // Book ile ilişki
    }
}
