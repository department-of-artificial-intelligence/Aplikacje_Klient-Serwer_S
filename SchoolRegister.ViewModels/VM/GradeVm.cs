using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;


namespace SchoolRegister.ViewModels.VM;

public class GradeVm
{
    public DateTime DateOfIssue { get; set; }

    [Required]
    public GradeScale GradeValue { get; set; }

    public int SubjectId { get; set; }
    public SubjectVm? Subject { get; set; }

    public int StudentId { get; set; }
    public StudentVm? Student { get; set; }
}