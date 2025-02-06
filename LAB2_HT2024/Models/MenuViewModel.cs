using System.ComponentModel;

namespace LAB2_HT2024.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [DisplayName("Dish Name:")]
        public string Name { get; set; }


        public int Price { get; set; }


        public bool Available { get; set; }
    }
}
