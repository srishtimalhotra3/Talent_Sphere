using System;

namespace TalentSphere.DTOs
{
    public class CreateInterviewDTO
    {
        public int ApplicationID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int InterviewerID { get; set; }
    }
}
