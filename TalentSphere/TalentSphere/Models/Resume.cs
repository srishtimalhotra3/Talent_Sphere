using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentSphere.Models
{
    [Table("Resume")]
    public class Resume
    {
        [Key]
        public int ResumeID { get; set; }

        [Required]
        public int CandidateID { get; set; }

        [ForeignKey("CandidateID")]
        public virtual User Candidate { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string FileURI { get; set; } = string.Empty;

        [Required]
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}