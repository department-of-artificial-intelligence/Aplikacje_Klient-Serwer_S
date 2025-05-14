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
    public class TeacherService : BaseService, ITeacherService
    {
        private UserManager<User> _userManager;

        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher,bool>> filterPredicate)
        {
            var teacher = DbContext
                    .Users.OfType<Teacher>()
                    .FirstOrDefault(filterPredicate);

            
            var teacherEntity = Mapper.Map<TeacherVm>(teacher);
            return teacherEntity;
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher,bool>> filterPredicate = null)
        {
            if(filterPredicate == null)
                filterPredicate = (x) => true;
            
            var teachers = DbContext.Users.OfType<Teacher>().Where(filterPredicate);
            var teachersVm = new List<TeacherVm>();

            foreach (var t in teachers)
            {
                teachersVm.Add(Mapper.Map<TeacherVm>(t));
            }
            
            return teachersVm;
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            try
            {
                if(getTeachersGroups == null)
                    throw new ArgumentNullException($"getTeachersGroups parameter is null");

                var teacher = DbContext
                    .Users.OfType<Teacher>()
                    .FirstOrDefault(t=>t.Id == getTeachersGroups.TeacherId);

                if(teacher == null)
                {
                    throw new InvalidOperationException(
                        $"No teacher with id {getTeachersGroups.TeacherId} found"
                    );
                }
                var groups = DbContext.Groups.Where(
                    g => 
                        g.SubjectGroups.FirstOrDefault(sg => sg.Subject.TeacherId == getTeachersGroups.TeacherId) != null
                    );

                var groupsVm = new List<GroupVm>();

                foreach (var g in groups)
                {
                    groupsVm.Add(Mapper.Map<GroupVm>(g));
                }
                
                return groupsVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
