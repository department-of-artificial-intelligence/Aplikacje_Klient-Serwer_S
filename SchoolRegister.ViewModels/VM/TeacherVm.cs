using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class TeacherVm
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int ParentId { get; set; }
    public double AverageGrade { get; set; }

    public IList<SubjectVm> Subjects { get; set; } = new List<SubjectVm>();
}