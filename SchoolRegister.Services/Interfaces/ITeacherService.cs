using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filter_predicate);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filter_predicate = null);
        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm get_teachers_groups);
    }
}