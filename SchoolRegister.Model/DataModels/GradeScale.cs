using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class GradeScale
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
}