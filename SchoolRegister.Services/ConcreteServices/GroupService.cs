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
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;

        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) 
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            if (addOrUpdateGroupVm == null) throw new ArgumentNullException(nameof(addOrUpdateGroupVm));

            var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);
            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                DbContext.Groups.Add(groupEntity);
            else
                DbContext.Groups.Update(groupEntity);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(groupEntity);
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
                student.GroupId = null; // Usunięcie przypisania
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            var existingLink = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == attachSubjectGroupVm.GroupId && sg.SubjectId == attachSubjectGroupVm.SubjectId);
            if (existingLink == null)
            {
                DbContext.SubjectGroups.Add(new SubjectGroup { GroupId = attachSubjectGroupVm.GroupId, SubjectId = attachSubjectGroupVm.SubjectId });
                DbContext.SaveChanges();
            }
            var group = DbContext.Groups.FirstOrDefault(g => g.Id == attachSubjectGroupVm.GroupId);
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm)
        {
            var link = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == detachSubjectVm.GroupId && sg.SubjectId == detachSubjectVm.SubjectId);
            if (link != null)
            {
                DbContext.SubjectGroups.Remove(link);
                DbContext.SaveChanges();
            }
            var group = DbContext.Groups.FirstOrDefault(g => g.Id == detachSubjectVm.GroupId);
            return Mapper.Map<GroupVm>(group);
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

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            var group = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null!)
        {
            var query = DbContext.Groups.AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<GroupVm>>(query);
        }
    }
}