using TalentSphere.Enums;

namespace TalentSphere.DTOs
{
    public class CreateComplianceRecordDTO
    {
        public int EmployeeID { get; set; }

        public CompilanceRecordType Type { get; set; }

        public string Result { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}
