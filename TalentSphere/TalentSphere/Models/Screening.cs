using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentSphere.Models
{
    [Table("Screening")]
    public class Screening
    {
        [Key]
        public int ScreeningID { get; set; }

        [Required]
        public int ApplicationID { get; set; }

        [ForeignKey("ApplicationID")]
        public virtual Application Application { get; set; } = null!;

        [Required]
        public int RecruiterID { get; set; }

        [ForeignKey("RecruiterID")]
        public virtual User Recruiter { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Result { get; set; } = string.Empty;  // Pass/Fail/+1

        public string? Notes { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}