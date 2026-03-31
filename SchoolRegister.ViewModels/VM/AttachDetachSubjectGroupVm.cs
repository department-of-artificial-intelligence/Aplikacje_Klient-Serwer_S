using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM;

public class AttachDetachSubjectGroupVm
{
    [Required]
    public int SubjectId { get; set; }

    [Required]
    public int GroupId { get; set; }
}