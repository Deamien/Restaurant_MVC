using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.ReservationViewModels
{
    public class AddReservationViewModel
    {
        [Required(ErrorMessage = "The field cannot be empty.")]
        [DisplayName("People:")]
        public int groupSize { get; set; }

        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "TableId is required")]
        [DisplayName("Available Tables:")]
        public int TableId { get; set; }

        [DisplayName("Reservation Starts (Date&Time)")]
        public DateTime reservationStart { get; set; }

        [DisplayName("Reservation Ends (Date&Time)")]
        public DateTime reservationEnd { get; set; }
    }
}
