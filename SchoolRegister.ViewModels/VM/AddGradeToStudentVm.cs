using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class AddGradeToStudentVm
    {
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public GradeScale GradeValue { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = null!;

        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;
    }
}