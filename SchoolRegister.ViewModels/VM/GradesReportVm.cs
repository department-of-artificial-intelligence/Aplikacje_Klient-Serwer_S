using System.Collections.Generic;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
    public string StudentName { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public IEnumerable<GradeVm> Grades { get; set; } = new List<GradeVm>();
    public double AverageGrade { get; set; }
    }
}