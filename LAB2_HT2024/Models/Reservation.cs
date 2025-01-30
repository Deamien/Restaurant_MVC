using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB2_HT2024.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Fältet kan inte vara tomt")]
        //[DisplayName("Antal personer")]
        public int GroupSize { get; set; }
        public int TableId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}

