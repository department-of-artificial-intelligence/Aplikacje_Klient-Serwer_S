using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM;

public class GradesReportVm
{
    public int StudentId { get; set; }
    public StudentVm? Student { get; set; }
    public IList<GradeVm> Grades { get; set; } = new List<GradeVm>();
}