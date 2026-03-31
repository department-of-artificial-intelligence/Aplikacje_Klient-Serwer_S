using System;

namespace SchoolRegister.ViewModels.VM;

public class GradeVm
{
    public int Id { get; set; }
    public double Value { get; set; } // Wartość oceny (np. 5.0, 4.5, 3.0)
    public int Weight { get; set; } // Waga oceny
    public string Description { get; set; } // Np. "Sprawdzian", "Aktywność"
    public DateTime DateIssued { get; set; }
    
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    
    public int TeacherId { get; set; }
    public string TeacherName { get; set; }
    
    public int StudentId { get; set; }
    public string StudentName { get; set; }
}