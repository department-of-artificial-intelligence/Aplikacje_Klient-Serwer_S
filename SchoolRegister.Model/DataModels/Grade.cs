using System;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
    public virtual DateTime DateOfIssue { get; set; } = DateTime.Now;
    public virtual GradeScale GradeValue { get; set; }

    public virtual int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public virtual int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Grade()
    {
    }
}