using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filter)
        {
            var entity = DbContext.Groups.FirstOrDefault(filter);
            return Mapper.Map<GroupVm>(entity);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filter = null)
        {
            var query = DbContext.Groups.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return Mapper.Map<IEnumerable<GroupVm>>(query);
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm vm)
        {
            var entity = Mapper.Map<Group>(vm);

            if (!vm.Id.HasValue || vm.Id == 0)
                DbContext.Groups.Add(entity);
            else
                DbContext.Groups.Update(entity);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(entity);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(s => s.Id == vm.StudentId);

            student.GroupId = vm.GroupId;
            DbContext.SaveChanges();

            return Mapper.Map<StudentVm>(student);
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(s => s.Id == vm.StudentId);

            student.GroupId = null;
            DbContext.SaveChanges();

            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            DbContext.SubjectGroups.Add(new SubjectGroup
            {
                GroupId = vm.GroupId,
                SubjectId = vm.SubjectId
            });

            DbContext.SaveChanges();
            return GetGroup(x => x.Id == vm.GroupId);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
        {
            var sg = DbContext.SubjectGroups
                .FirstOrDefault(x => x.GroupId == vm.GroupId && x.SubjectId == vm.SubjectId);

            DbContext.SubjectGroups.Remove(sg);
            DbContext.SaveChanges();

            return GetGroup(x => x.Id == vm.GroupId);
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == vm.SubjectId);
            subject.TeacherId = vm.TeacherId;

            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == vm.SubjectId);
            subject.TeacherId = null;
            DbContext.SaveChanges();

            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
    }
}