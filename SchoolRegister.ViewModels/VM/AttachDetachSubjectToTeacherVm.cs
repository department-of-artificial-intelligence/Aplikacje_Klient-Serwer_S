using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM;

public class AttachDetachSubjectToTeacherVm
{
    [Required]
    public int SubjectId { get; set; }

    [Required]
    public int TeacherId { get; set; }
}