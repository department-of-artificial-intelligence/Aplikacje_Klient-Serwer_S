using System;
namespace SchoolRegister.Model.DataModels;
public class Grade {
    public DateTime DateOfIssue { get; set; }
    public GradeValue GradeScale { get; set; }
    public Subject Subject {get;set;}
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public Grade(){}
}
