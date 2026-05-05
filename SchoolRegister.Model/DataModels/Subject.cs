using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public string? Description {get; set;}
    public virtual Teacher Teacher {get; set;}
    public int? TeacherId {get; set;}
    public virtual IList<Grade> Grades {get; set;}
    public virtual IList<SubjectGroup> SubjectGroups {get; set;}
    
}
