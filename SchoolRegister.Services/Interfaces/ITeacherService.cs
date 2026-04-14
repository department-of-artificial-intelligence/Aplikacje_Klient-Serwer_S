using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null);
        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups);
    }
}