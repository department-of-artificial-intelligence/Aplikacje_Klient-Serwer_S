using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<GradeVm> GetGrades(Expression<Func<Grade, bool>>? filter = null);
    }
}