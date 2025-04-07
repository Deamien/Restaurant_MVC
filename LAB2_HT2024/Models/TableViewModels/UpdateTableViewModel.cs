using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.TableViewModels
{
    public class UpdateTableViewModel
    {
        public int TableId { get; set; }

        [Required]
        public int seats { get; set; }
    }
}
