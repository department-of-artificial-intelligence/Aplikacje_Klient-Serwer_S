using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
    public DateTime DateOfIssue { get; set; } = DateTime.Now;
    public GradeScale GradeValue { get; set; }
    public virtual Subject Subject { get; set; } = null!;
    public int SubjectID { get; set; }
    public int StudentID { get; set; }
    public virtual Student Student { get; set; } = null!;

}
