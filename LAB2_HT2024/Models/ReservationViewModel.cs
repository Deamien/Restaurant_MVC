using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Fältet kan inte vara tomt")]
        [DisplayName("Antal personer")]
        public int groupSize { get; set; }

        [Required(ErrorMessage = "Please Choose a table")]
        [DisplayName("Available Tables:")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Enter your first name")]
        [DisplayName("First Name:")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        [DisplayName("Last Name:")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DisplayName("Phone Number:")]
        [Phone]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid Phone number")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email:")]
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string emailAddress { get; set; }

        [DisplayName("Reservation Starts (Date&Time)")]
        public DateTime reservationStart { get; set; }

        [DisplayName("Reservation Ends (Date&Time)")]
        public DateTime reservationEnd { get; set; }
    }
}

