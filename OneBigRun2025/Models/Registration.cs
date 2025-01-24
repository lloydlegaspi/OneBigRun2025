using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneBigRun2025.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationID { get; set; }

        [Required]
        [ForeignKey("Participant")]
        public int ParticipantID { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public Participant Participant { get; set; } // Navigation property
    }
}

