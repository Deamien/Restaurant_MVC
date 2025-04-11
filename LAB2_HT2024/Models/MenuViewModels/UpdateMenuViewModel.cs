using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.MenuViewModels
{
    public class UpdateMenuViewModel
    {
        [Required(ErrorMessage = "MenuItemId is required")]
        public int MenuItemId { get; set; }

        [DisplayName("Dish:")]
        [Required]
        public string name { get; set; }

        [DisplayName("Price:")]
        [Required]
        public int price { get; set; }

        [Required]
        public bool available { get; set; }
    }
}
