using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
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

        // Wstrzykujemy UserManager tak jak na diagramie UML
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) 
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
            return Mapper.Map<TeacherVm>(teacher);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null!)
        {
            var query = DbContext.Users.OfType<Teacher>().AsQueryable();
            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }
            return Mapper.Map<IEnumerable<TeacherVm>>(query);
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            if (getTeachersGroups == null) throw new ArgumentNullException(nameof(getTeachersGroups));

            var groups = DbContext.SubjectGroups
                .Where(sg => sg.Subject.TeacherId == getTeachersGroups.TeacherId)
                .Select(sg => sg.Group)
               // .Distinct()
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}