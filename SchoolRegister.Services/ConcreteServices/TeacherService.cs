using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _user_manager;

        public TeacherService(ApplicationDbContext db_context, IMapper mapper, ILogger logger, UserManager<User> user_manager)
            : base(db_context, mapper, logger)
        {
            _user_manager = user_manager;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filter_predicate)
        {
            if (filter_predicate == null)
                throw new ArgumentNullException("Filter predicate is null");

            var teacher_entity = DbContext.Users
                .OfType<Teacher>()
                .FirstOrDefault(filter_predicate);

            return Mapper.Map<TeacherVm>(teacher_entity);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filter_predicate = null)
        {
            var teacher_entities = DbContext.Users
                .OfType<Teacher>()
                .AsQueryable();

            if (filter_predicate != null)
                teacher_entities = teacher_entities.Where(filter_predicate);

            return Mapper.Map<IEnumerable<TeacherVm>>(teacher_entities);
        }

        public IEnumerable<GroupVm> GetTeacherGroups(TeachersGroupsVm getTeacherGroups)
        {
            if (getTeacherGroups == null)
                throw new ArgumentNullException("VM parameter is null");

            var groups = DbContext.SubjectGroups
                .Where(sg => sg.Subject.TeacherId == getTeacherGroups.TeacherId)
                .Select(sg => sg.Group)
                .Distinct()
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm get_teachers_groups)
        {
            if (get_teachers_groups == null)
                throw new ArgumentNullException("VM parameter is null");

            var subjectGroups = DbContext.SubjectGroups
                .Where(sg => sg.Subject.TeacherId == get_teachers_groups.TeacherId)
                .ToList();

            var groups = subjectGroups
                .Select(sg => sg.Group)
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}