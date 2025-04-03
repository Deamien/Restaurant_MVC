using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.MenuViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [DisplayName("Dish Name:")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Price:")]
        [Required]
        public int Price { get; set; }

        [Required]
        public bool Available { get; set; }
    }
}
