using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm? GetStudent(Expression<Func<Student, bool>> filter);

        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filter = null);
    }
}