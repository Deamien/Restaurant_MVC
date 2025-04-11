using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.TableViewModels
{
    public class AddTableViewModel
    {
        [Required(ErrorMessage = "Seats are required")]
        public int seats { get; set; }
    }
}
