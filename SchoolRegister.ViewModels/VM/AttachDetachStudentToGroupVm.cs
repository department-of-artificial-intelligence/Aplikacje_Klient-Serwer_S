using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachStudentToGroupVm
    {
        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public int StudentId { get; set; }
    }
}