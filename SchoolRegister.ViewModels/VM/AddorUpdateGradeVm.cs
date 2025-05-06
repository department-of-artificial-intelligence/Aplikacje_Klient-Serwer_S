using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateGradeVm
    {
        public int? Id { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public DateTime DateOfIssue { get; set; }

        [Required]
        public GradeScale GradeValue { get; set; }
    }
}
