using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeReportVm
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; } = null!;
        public string StudentLastName { get; set; } = null!;
        public double AverageGrade { get; set; }
        public IList<GradeVm> Grades { get; set; } = new List<GradeVm>();
    }
}