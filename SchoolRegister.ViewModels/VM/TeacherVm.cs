using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class TeacherVm
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public int ParentId { get; set; }
    public double AverageGrade { get; set; }
    public IDictionary<string, List<Subject>> Subjects { get; set; }
}