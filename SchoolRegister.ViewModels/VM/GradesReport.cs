using System.Collections.Generic;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public required string StudentName { get; set; }
        public IEnumerable<GradeVm> Grades { get; set; } = new List<GradeVm>();
        public string? GroupName { get; set; }  
        public double? AverageGrade { get; set; } 
    }
}