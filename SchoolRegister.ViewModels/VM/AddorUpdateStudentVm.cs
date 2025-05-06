using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateStudentVm
    {
        public int? Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public int GroupId { get; set; }
        public int ParentId { get; set; }
    }
}
