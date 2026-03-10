using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public string Title { get; set; } = string.Empty;
    public virtual IList<Subject> Subjects { get; set; }

    public Teacher()
    {
        Subjects = new List<Subject>();
    }
}
