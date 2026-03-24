using System;

public class Grade
{
    public int Id { get; set; }

    public DateTime DateOfIssue { get; set; } = DateTime.Now;

    public int GradeValue { get; set; }
    public GradeScale GradeScale { get; set; }

    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}