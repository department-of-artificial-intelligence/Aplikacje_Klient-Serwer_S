using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}