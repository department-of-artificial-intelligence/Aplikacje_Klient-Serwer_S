using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Subject, bool>> filterExpression = null);
        IEnumerable<GroupVm> GetTeachersGroups();
    }
}