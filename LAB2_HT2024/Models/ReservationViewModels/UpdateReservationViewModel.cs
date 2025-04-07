using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.ReservationViewModels
{
    public class UpdateReservationViewModel
    {
        [Required(ErrorMessage = "ReservationId cannot be empty")]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "The field cannot be empty.")]
        [DisplayName("People:")]
        public int groupSize { get; set; }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please Choose a table")]
        [DisplayName("Available Tables:")]
        public int TableId { get; set; }

        [DisplayName("Reservation Starts (Date&Time)")]
        [Required(ErrorMessage = "Please choose a time that fits you")]
        public DateTime reservationStart { get; set; }

        [DisplayName("Reservation Ends (Date&Time)")]
        [Required]
        public DateTime reservationEnd { get; set; }
    }
}
