using System;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int? Id {get;set;} = 0;
    [Required]
    public string Name{get;set;} = null!;
    public string Description{get;set;} = null!;
    public virtual IList<SubjectGroup> SubjectGroups{get;set;} = null!;
    public virtual Teacher Teacher{get;set;} = null!;
    public int? TeacherId{get;set;} = 0;
    public virtual IList<Grade> Grades{get;set;} = null!;
}