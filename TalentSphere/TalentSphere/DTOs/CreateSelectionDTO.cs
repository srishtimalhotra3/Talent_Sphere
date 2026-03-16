using System;

namespace TalentSphere.DTOs
{
    public class CreateSelectionDTO
    {
        public int ApplicationID { get; set; }
        public string Decision { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
