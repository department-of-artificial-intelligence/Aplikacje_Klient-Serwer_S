using System.Collections.Generic;
using SchoolRegister.Model.DataModels; // Upewnij się, że ten using tu jest dla GradeScale

namespace SchoolRegister.ViewModels.VM;

// --- MODELE DLA OCEN (GradeService) ---

public class AddGradeToStudentVm
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public GradeScale GradeValue { get; set; } 
    public int TeacherId { get; set; }
}

public class GradeVm
{
    public int Id { get; set; }
    public GradeScale GradeValue { get; set; }
    public int SubjectId { get; set; }
}

public class GetGradesReportVm
{
    public int StudentId { get; set; }
    public int GetterUserId { get; set; }
}

public class GradesReportVm
{
    public List<GradeVm> Grades { get; set; } = new List<GradeVm>();
}


// --- MODELE DLA GRUP (GroupService) ---

public class AddOrUpdateGroupVm
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
}

public class AttachDetachStudentToGroupVm
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
}

public class AttachDetachSubjectGroupVm
{
    public int SubjectId { get; set; }
    public int GroupId { get; set; }
}

public class AttachDetachSubjectToTeacherVm
{
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
}


// --- MODELE DLA NAUCZYCIELI (TeacherService) ---

public class TeacherVm
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Title { get; set; } = null!;
}

public class TeachersGroupsVm
{
    public int TeacherId { get; set; }
}