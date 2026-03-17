using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels;
public class Grade
{
    [Key]
    public DateTime DateOfIssue { get; set; }
    [Required]
    public GradeScale GradeValue { get; set; }
    public virtual Subject Subject { get; set; } = null!;
    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = null!;
    public Grade() {}
}