using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Subject
{

    public int Id {get; set;} 
    public string Name {get; set;} = null!;
    public string Descryption {get; set;} = null!;
    public int? TeacherId {get; set;} = null!;
    
    public virtual IList<SubjectGroup> SubjectGroups {get; set;} = null!;
    public virtual IList<Grade> Grades {get; set;} = null!;
    public virtual Teacher Teacher {get; set;} = null!;

    public Subject()
    {
        SubjectGroups = new List<SubjectGroup>();
        Grades = new List<Grade>();
    }

}