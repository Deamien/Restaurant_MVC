using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB2_HT2024.Models.CustomerViewModels
{
    public class GetCustomerViewModel
    {
        public int CustomerId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string emailAddress { get; set; }

        public string phoneNumber { get; set; }
    }
}
