namespace SchoolRegister.ViewModels.VM;

public class GradesReportVm
{
    public string? StudentName { get; set; }
    public IEnumerable<GradeVm>? Grades { get; set; }
}
