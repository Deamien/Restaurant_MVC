using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
    }
}
