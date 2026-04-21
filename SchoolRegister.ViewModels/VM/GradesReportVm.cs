using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;
using System.Collections.Generic;
using System.Collections;
namespace SchoolRegister.ViewModels.VM;

public class GradesReportVm
{
    public string StudentFullName { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public IEnumerable<GradeVm> Grades { get; set; } = new List<GradeVm>();
    public double AverageGrade { get; set; }
}