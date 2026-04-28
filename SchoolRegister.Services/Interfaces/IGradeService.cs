using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        GradeVm AddGradeToStudent(AddGradeToStudentVm vm);

        IEnumerable<GradeVm> GetGradesReportForStudent(GetGradesReportVm vm);
    }
}