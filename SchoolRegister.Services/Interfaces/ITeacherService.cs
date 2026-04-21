using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> predicateFilter);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null);
        IEnumerable<GroupVM> GetTeachersGroups(TeachersGroupsVm getTeachersGroup);
    }
}