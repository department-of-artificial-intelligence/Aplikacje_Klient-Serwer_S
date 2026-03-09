using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
    public DateTime DateOfIssue { get; set; }
    public GradeScale GradeValue { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
    public int SubjectID { get; set; }
    public int StudentID { get; set; }
    public Student Student { get; set; } = null!;

}
