using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachSubjectGroupVm
    {
        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public int SubjectId { get; set; }
    }
}