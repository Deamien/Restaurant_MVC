using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.TableViewModels
{
    public class AddTableViewModel
    {
        [Required(ErrorMessage = "Seats are required")]
        [Range(1, 12, ErrorMessage = "Number of seats must be between 1 and 12.")]
        public int seats { get; set; }
    }
}
