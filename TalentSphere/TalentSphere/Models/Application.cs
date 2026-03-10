using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentSphere.Models
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        [Required]
        public int JobID { get; set; }

        [ForeignKey("JobID")]
        public virtual Job Job { get; set; } = null!;

        [Required]
        public int CandidateID { get; set; }

        [ForeignKey("CandidateID")]
        public virtual User Candidate { get; set; } = null!;

        [Required]
        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
    
}
