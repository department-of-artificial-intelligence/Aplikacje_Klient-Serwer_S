using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM;

public class GroupVm
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    // Opcjonalnie: listy powiązanych encji do wyświetlenia na widoku szczegółów grupy
    public List<StudentVm> Students { get; set; } = new List<StudentVm>();
    public List<SubjectVm> Subjects { get; set; } = new List<SubjectVm>();
}