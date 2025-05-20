using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Grade{
    [Key]
    public int GradeId {get; set;}
    public DateTime DateOfIssue {get;set;} 
    public GradeScale GradeValue {get;set;}
    [ForeignKey("SubjectId")]
    public virtual Subject Subject {get;set;} =null!;
    public int SubjectId{get;set;}
    public int StudentId {get;set;}
    [ForeignKey("StudentId")]
    public virtual Student Student {get;set;} =null!;
}