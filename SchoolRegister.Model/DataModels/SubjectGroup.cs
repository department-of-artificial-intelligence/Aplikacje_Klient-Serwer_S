using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
    public virtual Subject Subject { get; set; } = null!;
    public virtual int SubjectId { get; set; }
    public virtual Group Group { get; set; } = null!;
    public virtual int GroupId { get; set; }
}