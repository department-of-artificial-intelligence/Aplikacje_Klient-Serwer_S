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
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;

        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger<GroupService> logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            var group = Mapper.Map<Group>(addOrUpdateGroupVm);
            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0) DbContext.Groups.Add(group);
            else DbContext.Groups.Update(group);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            var group = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
        {
            var query = DbContext.Groups.AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<GroupVm>>(query.ToList());
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);
            if (student != null)
            {
                student.GroupId = attachStudentToGroupVm.GroupId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == detachStudentToGroupVm.StudentId);
            if (student != null)
            {
                student.GroupId = null;
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            var subjectGroup = new SubjectGroup
            {
                GroupId = attachSubjectGroupVm.GroupId,
                SubjectId = attachSubjectGroupVm.SubjectId
            };
            DbContext.SubjectGroups.Add(subjectGroup);
            DbContext.SaveChanges();
            return GetGroup(g => g.Id == attachSubjectGroupVm.GroupId);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectGroupVm)
        {
            var subjectGroup = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == detachSubjectGroupVm.GroupId && sg.SubjectId == detachSubjectGroupVm.SubjectId);
            if (subjectGroup != null)
            {
                DbContext.SubjectGroups.Remove(subjectGroup);
                DbContext.SaveChanges();
            }
            return GetGroup(g => g.Id == detachSubjectGroupVm.GroupId);
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
            if (subject != null)
            {
                subject.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
            if (subject != null)
            {
                subject.TeacherId = null;
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }
    }
}