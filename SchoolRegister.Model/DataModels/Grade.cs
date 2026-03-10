using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
    [Key]
    public int Id { get; set; }
    public DateTime DateOfIssue { get; set; }
    public GradeScale GradeValue { get; set; }

    [Required]
    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public virtual Subject? Subject { get; set; }

    [Required]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student? Student { get; set; }
}
