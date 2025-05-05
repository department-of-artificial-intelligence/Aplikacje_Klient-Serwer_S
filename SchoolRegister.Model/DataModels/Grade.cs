using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Grade{
    public DateTime DateOfIssue {get;set;} 
    public GradeScale GradeValue {get;set;}
    [ForeignKey("SubjectId")]
    public Subject Subject {get;set;} =null!;
    public int SubjectId{get;set;}
    public int StudentId {get;set;}
    [ForeignKey("StudentId")]
    public Student Student {get;set;} =null!;
}