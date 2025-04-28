using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>>? filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterExpression = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm teachersGroupsVm)
        {
            throw new NotImplementedException();
        }
    }
}