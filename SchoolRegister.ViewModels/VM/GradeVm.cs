using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public GradeScale GradeValue { get; set; }
        public string SubjectName { get; set; } = null!;
        public string StudentFirstName { get; set; } = null!;
        public string StudentLastName { get; set; } = null!;
    }
}