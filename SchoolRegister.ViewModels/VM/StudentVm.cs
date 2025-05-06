using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class StudentVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ParentName { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public double AverageGrade { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IDictionary<string, double> AverageGradePerSubject { get; set; }
        public IDictionary<string, List<GradeScale>> GradesPerSubject { get; set; }
    }
}