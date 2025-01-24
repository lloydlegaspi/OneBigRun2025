using System.ComponentModel.DataAnnotations;

namespace OneBigRun2025.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        public string ShirtSize { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public ICollection<Registration> Registrations { get; set; } // Navigation property
    }
}

