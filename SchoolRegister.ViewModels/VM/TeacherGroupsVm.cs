using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM;


public class TeacherGroupsVm
{
    public int TeacherId { get; set; }

    [Required]
    public string TeacherName { get; set; } = null!;

    public string? Title { get; set; }

    public IList<GroupVm> Groups { get; set; } = new List<GroupVm>();
}