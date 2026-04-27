using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public StudentVm Student { get; set; } = null!;
        public IEnumerable<GradeVm> Grades { get; set; } = new List<GradeVm>();
    }
}