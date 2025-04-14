using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels;
[Table("Group")]
public class Group{
    [Key]
   public int Id {get;set;}
    [Required]
   public string Name {get;set;}  =null!;
   public virtual IList<Student> Students {get;set;} =null!;
   public virtual IList<SubjectGroup> SubjectGroups {get;set;} =null!;

}