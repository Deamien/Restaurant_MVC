using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LAB1_HT2024.Models.DTOs.CustomerDTOs
{
    public class UpdateCustomerViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Enter your first name")]
        [DisplayName("First Name:")]
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        [DisplayName("Last Name:")]
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email:")]
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DisplayName("Phone Number:")]
        [Phone]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid Phone number")]
        public string phoneNumber { get; set; }
    }
}
