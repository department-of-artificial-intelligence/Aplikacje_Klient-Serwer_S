using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        public TeacherVm GetTeacher(Expression<Func<Teacher,bool>> filterPredicate)
        {
            if (filterPredicate == null)
                throw new ArgumentNullException("Filter predicate is null");

            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
            return Mapper.Map<TeacherVm>(teacherEntity);
        }
        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher,bool>> filterPredicate = null)
        {
            var teacherEntities = DbContext.Users.OfType<Teacher>().AsQueryable();

            if (filterPredicate != null)
                teacherEntities = teacherEntities.Where(filterPredicate);

            return Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
        }
        public IEnumerable<GroupVm> GetTeacherGroups(TeacherGroupsVm getTeacherGroups)
        {
            if(getTeacherGroups == null)
                throw new ArgumentNullException("VM parameter is null");
                
            var groups = DbContext.SubjectGroups
                .Where(sg => sg.Subject.TeacherId == getTeacherGroups.TeacherId)
                .Select(sg => sg.Group)
                .Distinct()
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}