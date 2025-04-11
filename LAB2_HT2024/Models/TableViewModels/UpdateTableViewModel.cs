using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.TableViewModels
{
    public class UpdateTableViewModel
    {
        [Required(ErrorMessage = "TableId is required")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Seats are required")]
        public int seats { get; set; }
    }
}
