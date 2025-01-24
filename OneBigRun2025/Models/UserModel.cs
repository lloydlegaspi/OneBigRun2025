using System.ComponentModel.DataAnnotations;

namespace OneBigRun2025.Models
{
    public class UserModel
    {
        // User Details
        public int ParticipantID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Please enter a valid age.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Shirt size is required.")]
        [StringLength(10, ErrorMessage = "Shirt size cannot be longer than 10 characters.")]
        public string ShirtSize { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters.")]
        public string PhoneNumber { get; set; }

        // Registration Details
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        // Set default registration date to current date and time
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}

