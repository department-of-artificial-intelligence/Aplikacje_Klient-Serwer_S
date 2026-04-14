using System;
using DataModels; 

namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public int Id { get; set; }
        public GradeScale GradeValue { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int StudentId { get; set; }
    }
}