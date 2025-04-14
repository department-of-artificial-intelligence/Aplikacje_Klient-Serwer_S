using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;
public class Teacher : User
{
public string Title { get; set; } =null!;
  
public virtual IList<Subject> Subjects {get;set;} = new List<Subject>();
}
