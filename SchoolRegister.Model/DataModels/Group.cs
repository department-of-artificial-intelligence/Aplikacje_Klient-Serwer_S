using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;

public class Group
{
    [Key]
    public int Id{get;set;}
    [Required]
    public string Name{get;set;}
    public virtual IList<Student> Students{get;set;}
    public virtual IList<SubjectGroup> SubjectGroups{get;set;}
}