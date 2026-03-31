using System;
using System.Collections.Generic;

namespace SchoolRegister.ViewModels.VM;

public class GradesReportVm
{
    public int StudentId { get; set; }
    public string StudentFullName { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.Now;
    
    public double AverageGrade { get; set; } 
    
    public List<GradeVm> Grades { get; set; } = new List<GradeVm>();
}