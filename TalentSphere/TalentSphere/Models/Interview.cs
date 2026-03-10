using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace TalentSphereAPI.Models
{
    public class Interview
    {
        [Key]
        public int InterviewID { get; set; }
 
        [Required]
        public int ApplicationID { get; set; }
 
        [Required]
        public DateTime Date { get; set; }
 
        [Required]
        public string Time { get; set; }
 
        [Required]
        public int InterviewerID { get; set; }
 
        [Required]
        public string Status { get; set; }
 
        // Navigation Properties
 
        [ForeignKey("ApplicationID")]
        public Application Application { get; set; }
 
        [ForeignKey("InterviewerID")]
        public User Interviewer { get; set; }
    }
}
 