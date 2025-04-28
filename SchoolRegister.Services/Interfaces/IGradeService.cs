using System.Linq.Expressions;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm);
        GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm);
    }
}
