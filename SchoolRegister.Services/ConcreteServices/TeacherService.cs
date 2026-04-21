using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            :base(dbContext, mapper, logger) {}
        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate) {}

        public IEnumerable<GroupVM> GetTeachersGroups(TeachersGroupsVm getTeachersGroup)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null) {}

        IEnumerable<TeacherVm> ITeacherService.GetTeachers(Expression<Func<Teacher, bool>> filterPredicate)
        {
            return GetTeachers(filterPredicate);
        }

        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupVm getTeachersGroups) {}
    }
}