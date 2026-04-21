namespace SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;
 public class GradeVm
 {
    public DateTime DateOfIssue { get; set; }
    public GradeScale GradeValue { get; set; }
    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; } = default!;
    public int StudentId { get; set; }
    public virtual Student Student { get; set; } = default!;
 }