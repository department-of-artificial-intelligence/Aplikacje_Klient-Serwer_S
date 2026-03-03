using SchoolRegister.Model.DataModels;
using System;

public class Grade
{
    public DateTime DateOfIssue { get; set; }
    public int GradeValue { get; set; }
    public GradeScale GradeScale { get; set; }
    public Subject Subject { get; set; }
    public Student Student { get; set; }
}