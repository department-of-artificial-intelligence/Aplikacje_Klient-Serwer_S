using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM
{
    public class TeacherVm
    {

        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public virtual IList<Subject> Subjects { get; set; }
    }
}