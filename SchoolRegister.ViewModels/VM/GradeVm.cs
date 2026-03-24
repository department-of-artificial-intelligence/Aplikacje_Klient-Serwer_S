using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class GradeVm
{
    public int Id { get; set; }
    public DateTime DateOfIssue { get; set; }
    public string? SubjectName { get; set; }
    public string? StudentName { get; set; }
    public GradeScale GradeValue { get; set; }
}
