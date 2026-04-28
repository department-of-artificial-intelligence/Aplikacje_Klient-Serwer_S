using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        GradeVm AddGradeToStudent(AddGradeToStudentVm add_grade_to_student_vm);
        GradesReportVm GetGradesReportForStudent(GetGradesReportVm get_grades_vm);
    }
}