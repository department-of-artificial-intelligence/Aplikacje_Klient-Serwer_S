using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.Model.DataModels;
public class Subject
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual IList<SubjectGroup> SubjectGroups { get; set; } = default!;
    public virtual Teacher Teacher { get; set; } = null!;
    public int? TeacherId { get; set; }
    public virtual IList<Grade> Grades { get; set; } = default!;
    public Subject() {}
}