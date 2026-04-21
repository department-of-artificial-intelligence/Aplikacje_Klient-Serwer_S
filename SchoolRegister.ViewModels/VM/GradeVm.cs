using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolRegister.Model.DataModels;
namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public int Id { get; set; }
        public GradeScale GradeValue { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}