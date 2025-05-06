using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateGroupVm
    {
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public int? TeacherId { get; set; }

        public IList<int> StudentIds { get; set; } = new List<int>();
    }
}
