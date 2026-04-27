using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class TeacherVm
{
    public string Title { get; set; } = null!;
    public virtual IList<Subject> Subjects { get; set; }
}