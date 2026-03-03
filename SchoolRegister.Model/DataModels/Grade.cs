using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Grade
{
    public DateTime DateOfIssue { get; set; } = DateTime.Now;

    public GradeScale GradeValue { get; set; } = null!;

    public Subject subject { get; set; } = null!;

    public int SubjectId { get; set; } = 0;

    public int StudentId { get; set; } = 0;

    public Student student { get; set; } = null!;

    public Grade()
    {

    }
}