using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filter);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filter = null);
        IEnumerable<string> GetTeachersGroups(TeachersGroupsVm vm);
    }
}