using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Subject{
    [Key]
    public int Id {get;set;}
    [Required]
    public string Name{get;set;} =null!;
    public string Description{get;set;} =null!;
    public IList<SubjectGroup> SubjectGroups {get;set;} = new List<SubjectGroup>();
    public Teacher Teacher{get;set;} =null!;
    public int? TeacherId {get;set;}
    public IList<Grade> Grades {get;set;} =null!;
}