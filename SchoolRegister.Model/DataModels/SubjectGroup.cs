using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;
public class SubjectGroup
{
    public int SubjectId { get; set; }
    public virtual  Subject Subject { get; set; }
    public int GroupId { get; set; }
    public virtual  Group Group { get; set; } 

    public SubjectGroup()
    {
        
    }
}
