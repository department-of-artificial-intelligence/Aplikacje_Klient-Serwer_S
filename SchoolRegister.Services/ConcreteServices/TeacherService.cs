using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;

        public TeacherService(
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            ILogger logger, 
            UserManager<User> userManager) 
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            var teacher = DbContext.Set<Teacher>().FirstOrDefault(filterPredicate);
            return Mapper.Map<TeacherVm>(teacher);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            var query = DbContext.Set<Teacher>().AsQueryable();
            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }
            return Mapper.Map<IEnumerable<TeacherVm>>(query.ToList());
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
           
            var groups = DbContext.Set<SubjectGroup>() 
                .Where(g => g.Subject.TeacherId == getTeachersGroups.TeacherId)
                .ToList();
                
            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}