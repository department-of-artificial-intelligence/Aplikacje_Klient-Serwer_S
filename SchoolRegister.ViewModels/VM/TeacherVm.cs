using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class TeacherVm
{
    public virtual IList<Subject> Subjects { get; set; } = null!;

    public string Title {get; set;} = null!;


}