namespace SchoolRegister.ViewModels.VM;
public class GradesReportVm
{
    public string StudentName {get; set;} = null!;
    public string GroupName { get; set; } = null!;
    public double AverageGrade {get; set;}
    public IList<GradeVm> Grades {get; set;} = new List<GradeVm>();
    
}