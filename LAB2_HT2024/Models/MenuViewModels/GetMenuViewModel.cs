using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.MenuViewModels
{
    public class GetMenuViewModel
    {
        public int MenuItemId { get; set; }

        public string name { get; set; }

        public int price { get; set; }

        public bool available { get; set; }
    }
}
