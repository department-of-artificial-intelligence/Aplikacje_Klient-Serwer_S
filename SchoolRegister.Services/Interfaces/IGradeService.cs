using System.Threading.Tasks;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        Task<GradeVm> AddGradeToStudentAsync(AddGradeToStudentVm addGradeToStudentVm, int teacherUserId);
        Task<GradesReportVm> GetGradesForStudentAsync(int userId);
    }
}
