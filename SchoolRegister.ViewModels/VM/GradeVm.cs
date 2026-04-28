using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class GradeVm
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }

    public DateTime DateOfIssue { get; set; }

    public GradeScale Value { get; set; }
}