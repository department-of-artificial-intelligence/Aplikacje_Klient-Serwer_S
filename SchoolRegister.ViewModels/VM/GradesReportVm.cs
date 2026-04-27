using System.Collections.Generic;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public IList<GradeVm> Grades { get; set; } = new List<GradeVm>();
    }
}