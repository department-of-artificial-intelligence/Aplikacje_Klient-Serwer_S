using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException($"FilterPredicate is null");

                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);

                return teacherVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            try
            {
                var teacherEntities = DbContext.Users.OfType<Teacher>().AsQueryable();

                if (filterPredicate != null)
                    teacherEntities = teacherEntities.Where(filterPredicate);

                var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);

                return teacherVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            try
            {
                if (getTeachersGroups == null)
                    throw new ArgumentNullException($"getTeachersGroups is null");

                var groups = DbContext.SubjectGroups
                    .Include(sg => sg.Group)
                    .Include(sg => sg.Subject)
                    .Where(sg => sg.Subject.TeacherId == getTeachersGroups.TeacherId)
                    .Select(sg => sg.Group)
                    .Distinct()
                    .ToList();

                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groups);

                return groupVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}