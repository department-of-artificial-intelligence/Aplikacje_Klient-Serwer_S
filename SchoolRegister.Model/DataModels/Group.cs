using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.Model.DataModels;
public class Group
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public IList<Student> Students { get; set; } = new List<Student>();
    public IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public Group() { }
}