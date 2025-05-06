using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateTeacherVm
    {
        public int? Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public IList<int> SubjectIds { get; set; } = new List<int>();  
    }
}
