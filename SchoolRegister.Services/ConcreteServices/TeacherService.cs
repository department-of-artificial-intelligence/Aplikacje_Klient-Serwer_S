using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;

        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger<TeacherService> logger, UserManager<User> userManager) 
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
            return Mapper.Map<TeacherVm>(teacher);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            var query = DbContext.Users.OfType<Teacher>().AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<TeacherVm>>(query.ToList());
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == getTeachersGroups.TeacherId);
            if (teacher == null) return new List<GroupVm>();

            var groups = teacher.Subjects.SelectMany(s => s.SubjectGroups).Select(sg => sg.Group);
            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}