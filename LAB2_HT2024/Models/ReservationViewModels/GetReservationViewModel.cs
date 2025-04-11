namespace LAB2_HT2024.Models.ReservationViewModels
{
    public class GetReservationViewModel
    {
        public int ReservationId { get; set; }

        public int groupSize { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime reservationStart { get; set; }

        public DateTime reservationEnd { get; set; }

        public int CustomerId { get; set; }

        public int TableId { get; set; }
    }
}
