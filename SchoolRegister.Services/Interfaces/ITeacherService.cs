using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherVm GetTeacher(Expression<Func<Teacher, bool>>? filterExpression);
        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterExpression = null);
        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm teachersGroupsVm);
    }
}