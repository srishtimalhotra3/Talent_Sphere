using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace TalentSphere.Models
{
    public class ComplianceRecord
    {
        [Key]
        public int ComplianceID { get; set; }
 
        [Required]
        public int EmployeeID { get; set; }
 
        [Required]
        public string Type { get; set; } // Policy / Document
 
        public string Result { get; set; }
 
        public DateTime Date { get; set; }
 
        public string Notes { get; set; }
 
        // Navigation Property
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}