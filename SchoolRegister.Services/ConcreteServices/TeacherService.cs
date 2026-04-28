using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : BaseService(dbContext, mapper, logger), ITeacherService
    {
        private readonly UserManager<User> _userManager = userManager;

        public TeacherVm? GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            try
            {
                var teacher = DbContext.Users
                    .OfType<Teacher>()
                    .FirstOrDefault(filterPredicate);

                if (teacher == null)
                    return null;

                return Mapper.Map<TeacherVm>(teacher);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetTeacher");
                throw;
            }
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterPredicate = null)
        {
            try
            {
                var teachers = DbContext.Users
                    .OfType<Teacher>()
                    .AsQueryable();

                if (filterPredicate != null)
                    teachers = teachers.Where(filterPredicate);

                return Mapper.Map<IEnumerable<TeacherVm>>(teachers);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetTeachers");
                throw;
            }
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm request)
        {
            try
            {
                var teacher = DbContext.Users
                    .OfType<Teacher>()
                    .Include(t => t.Subjects)
                        .ThenInclude(s => s.SubjectGroups)
                            .ThenInclude(sg => sg.Group)
                    .FirstOrDefault(t => t.Id == request.TeacherId);

                if (teacher == null)
                    return [];

                var groups = teacher.Subjects
                    .SelectMany(s => s.SubjectGroups)
                    .Select(sg => sg.Group)
                    .ToList();

                return Mapper.Map<IEnumerable<GroupVm>>(groups);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetTeachersGroups");
                throw;
            }
        }
    }
}