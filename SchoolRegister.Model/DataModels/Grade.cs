using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Grade
{
    public DateTime DateOfIssue {get; set; }
    public GradeScale GradeValue{get; set;}
    public int SubjectId {get; set;}
    public virtual Subject Subject{get;set;}
    public int StudentId {get;set;}
    public virtual Student Student{get; set;}
    public Grade(){ }
}