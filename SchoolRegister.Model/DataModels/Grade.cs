using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

public class Grade
{
    [Key]
    public int GradeId { get; set; }
    public DateTime DateOfIssue { get; set; } = DateTime.Now;
    public GradeScale GradeValue { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
