using System.ComponentModel.DataAnnotations;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.ViewModels.VM
{
    public class GetGradesReportVm
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int GetterUserId { get; set; }
    }
}