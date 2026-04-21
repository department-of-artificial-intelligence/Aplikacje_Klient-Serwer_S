
namespace SchoolRegister.ViewModels.VM;
public class GradeVm
{
    public DateTime DateOfIssue { get; set; }
    public int Id { get; set; }
    public double GradeValue {get; set;}
    public int SubjectId {get; set;}
    public int StudentId {get; set;}
    public string SubjectName { get; set; } = null!;
 }