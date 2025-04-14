using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
public virtual Subject Subject{get;set;}
[ForeignKey("Subject")]
public int SubjectId{get;set;}
public virtual Group Group{get;set;}
[ForeignKey("Group")]
public int GroupId{get;set;}

}