using System.Collections.Generic;                   
using SchoolRegister.Model.DataModels;                

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public int StudentId { get; set; }
        public IDictionary<string, List<GradeScale>> GradesPerSubject { get; set; } = null!;
        public IDictionary<string, double> AverageGradePerSubject { get; set; } = null!;
    }
}