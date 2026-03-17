using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;
namespace SchoolRegister.Model.DataModels;


public class Grade
{
    [Key]
    public DateTime DateOfIssue { get; set; }
    [Required]
    public GradeScale GradeValue { get; set; }

    public virtual Subject Subject { get; set; }

    public int SubjectId { get; set; }

    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    public Grade() { }

}