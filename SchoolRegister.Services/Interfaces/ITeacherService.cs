using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher,bool>> filterPredicate);
        IEnumerable<TeacherVm> GetTeachers (Expression<Func<Teacher, bool>> filterPredicate = null);
        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeacherGroups);
    }
}