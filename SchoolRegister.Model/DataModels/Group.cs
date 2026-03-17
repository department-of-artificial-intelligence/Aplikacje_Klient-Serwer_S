using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Group
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public virtual IList<Student> Students { get; set; } = null!;
    public virtual IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public Group()
    {
        Students = new List<Student>();
        SubjectGroups = new List<SubjectGroup>();
    }


}

