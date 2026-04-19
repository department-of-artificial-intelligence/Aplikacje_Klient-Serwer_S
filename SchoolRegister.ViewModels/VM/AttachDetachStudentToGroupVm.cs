using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachStudentToGroupVm
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
    }
}