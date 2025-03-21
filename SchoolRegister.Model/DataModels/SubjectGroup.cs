using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{

    public Subject Subject { get; set; }= null!;
    

    public int SubjectId { get; set; }
    public virtual Group Group { get; set; }= null!;
    public int GroupId {get; set;}
}