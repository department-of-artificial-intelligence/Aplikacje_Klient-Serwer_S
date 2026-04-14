using System.Collections.Generic;
using DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string GroupName { get; set; }
        public string ParentName { get; set; }
        public IDictionary<string, List<GradeScale>> StudentGradesPerSubject { get; set; }
        public IDictionary<string, double> StudentAverageGradePerSubject { get; set; }
    }
}