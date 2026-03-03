using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
    public Subject subject { get; set; } = null!;

    public int SubjectId { get; set; } = 0;

    public Group group { get; set; } = null!;

    public int GroupId { get; set; } = 0;

    public SubjectGroup()
    {

    }
}