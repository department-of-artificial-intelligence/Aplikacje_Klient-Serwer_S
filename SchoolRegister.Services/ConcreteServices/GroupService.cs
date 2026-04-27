using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using System.Linq;

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
            var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);
            if (groupEntity.Id == 0)
                DbContext.Groups.Add(groupEntity);
            else
                DbContext.Groups.Update(groupEntity);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(groupEntity);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(x => x.Id == attachStudentToGroupVm.StudentId);

            if (student == null)
                throw new InvalidOperationException("Student not found.");

            student.GroupId = attachStudentToGroupVm.GroupId;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }
        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectToGroupVm)
        {
            var subjectGroup = new SubjectGroup
            {
                SubjectId = attachSubjectToGroupVm.SubjectId,
                GroupId = attachSubjectToGroupVm.GroupId
            };
            DbContext.SubjectGroups.Add(subjectGroup);
            DbContext.SaveChanges();

            var group = DbContext.Groups.FirstOrDefault(x => x.Id == attachSubjectToGroupVm.GroupId);
            if (group == null)
                throw new InvalidOperationException("Group not found.");

            return Mapper.Map<GroupVm>(group);
        }
        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachSubjectToTeacherVm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachSubjectToTeacherVm.SubjectId);

            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            subject.TeacherId = attachSubjectToTeacherVm.TeacherId;
            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(x => x.Id == detachStudentToGroupVm.StudentId);

            if (student == null)
                throw new InvalidOperationException("Student not found.");

            student.GroupId = null;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }
        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectFromGroupVm)
        {
            var subjectGroup = DbContext.SubjectGroups
                .FirstOrDefault(x => x.SubjectId == detachSubjectFromGroupVm.SubjectId && x.GroupId == detachSubjectFromGroupVm.GroupId);

            if (subjectGroup == null)
                throw new InvalidOperationException("SubjectGroup not found.");

            DbContext.SubjectGroups.Remove(subjectGroup);
            DbContext.SaveChanges();

            var group = DbContext.Groups.FirstOrDefault(x => x.Id == detachSubjectFromGroupVm.GroupId);
            if (group == null)
                throw new InvalidOperationException("Group not found.");

            return Mapper.Map<GroupVm>(group);
        }
        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachSubjectToTeacherVm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == detachSubjectToTeacherVm.SubjectId);

            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            subject.TeacherId = null;
            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            var group = DbContext.Groups.FirstOrDefault(filterPredicate);

            if (group == null)
                throw new InvalidOperationException("Group not found.");

            return Mapper.Map<GroupVm>(group);
        }
        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
        {
            var groups = DbContext.Groups.AsQueryable();
            if (filterPredicate != null)
                groups = groups.Where(filterPredicate);

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
    }
}