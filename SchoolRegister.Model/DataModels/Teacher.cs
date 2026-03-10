using SchoolRegister.Model.DataModels;
namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public string Title { get; set; }

    public IList<Subject> Subjects { get; set; }

    public Teacher() { }

}