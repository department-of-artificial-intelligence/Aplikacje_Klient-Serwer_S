using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;

public class Grade
{

    public int SubjectId { get; set; }
    public DateTime DateOfIssue { get; set; }
    public GradeScale GradeValue { get; set; }

    
   

    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; } = null!;

    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public  virtual Student Student { get; set; } = null!;
}
