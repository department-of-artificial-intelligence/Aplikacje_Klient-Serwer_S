using System.Linq.Expressions;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student,bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student,bool>> filterPredicate = null);
    }
}
