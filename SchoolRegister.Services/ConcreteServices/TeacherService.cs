using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SchoolRegister.Services.ConcreteServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolRegister.Services.Interfaces;


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

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
            return Mapper.Map<TeacherVm>(teacherEntity);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            var teachersQuery = DbContext.Users.OfType<Teacher>().AsQueryable();
            if (filterPredicate != null)
            {
                teachersQuery = teachersQuery.Where(filterPredicate);
            }
            return Mapper.Map<IEnumerable<TeacherVm>>(teachersQuery.ToList());
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            var allSubjectGroups = DbContext.SubjectGroups
                .Include(sg => sg.Subject)
                .Include(sg => sg.Group)
                .ToList();

            var teacherGroups = allSubjectGroups
                .Where(sg => sg.Subject != null && sg.Subject.TeacherId == getTeachersGroups.TeacherId)
                .Select(sg => sg.Group)
                .Where(g => g != null)
                .Distinct()
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(teacherGroups);
        }
    }
}