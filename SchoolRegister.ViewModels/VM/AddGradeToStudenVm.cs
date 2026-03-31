using System;

namespace SchoolRegister.ViewModels.VM;

public class AddGradeToStudentVm
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public double GradeValue { get; set; }
    public int Weight { get; set; } = 1;
    public string Description { get; set; }
}