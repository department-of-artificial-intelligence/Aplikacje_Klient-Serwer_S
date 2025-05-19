using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterExpression = null);
        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm vm);
    }
}