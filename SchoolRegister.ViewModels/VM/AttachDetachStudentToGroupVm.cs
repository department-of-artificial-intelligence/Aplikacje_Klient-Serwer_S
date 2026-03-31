using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM;

public class AttachDetachStudentToGroupVm
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int GroupId { get; set; }
}