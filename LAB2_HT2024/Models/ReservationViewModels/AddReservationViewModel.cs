using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LAB2_HT2024.Models.ReservationViewModels
{
    public class AddReservationViewModel
    {
        [Required(ErrorMessage = "The field cannot be empty.")]
        [DisplayName("People:")]
        public int groupSize { get; set; }

        [Key]
        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Please Choose a table")]
        [DisplayName("Available Tables:")]
        public int TableId { get; set; }
        
        public DateTime reservationStart { get; set; }
        public DateTime reservationEnd { get; set; }
    }
}
