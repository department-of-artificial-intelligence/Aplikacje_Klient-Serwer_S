using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;

        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression)
        {
            var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterExpression);
            return Mapper.Map<TeacherVm>(teacher);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterExpression = null)
        {
            var query = DbContext.Users.OfType<Teacher>().AsQueryable();
            if (filterExpression != null)
                query = query.Where(filterExpression);

            return Mapper.Map<IEnumerable<TeacherVm>>(query);
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm vm)
        {
            var teacher = DbContext.Users.OfType<Teacher>()
                .Include(t => t.Subjects)
                .ThenInclude(s => s.SubjectGroups)
                .ThenInclude(sg => sg.Group)
                .FirstOrDefault(t => t.Id == vm.TeacherId);

            var groups = teacher?.Subjects?
                .SelectMany(s => s.SubjectGroups.Select(sg => sg.Group));
                

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }

    }
}
