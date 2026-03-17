using SchoolRegister.Model.DataModels;
using System;
using System.Linq;

public class Grade
{
    public DateTime DateOfIssue { get; set; }
    public int GradeValue { get; set; }
    public GradeScale GradeScale { get; set; }
    public Subject Subject { get; set; }
    public Student Student { get; set; }
    public Grade()
    {
        DateOfIssue = DateTime.Now;
    }

    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}