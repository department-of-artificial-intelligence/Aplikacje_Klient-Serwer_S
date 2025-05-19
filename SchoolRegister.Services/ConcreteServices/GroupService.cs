using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm groupVm)
        {
            var groupEntity = Mapper.Map<Group>(groupVm);
            if (!groupVm.Id.HasValue || groupVm.Id == 0)
                DbContext.Groups.Add(groupEntity);
            else
                DbContext.Groups.Update(groupEntity);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(groupEntity);
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>>? filter)
        {
            var entity = DbContext.Groups.Include(g => g.Students)
                                         .Include(g => g.SubjectGroups)
                                         .ThenInclude(sg => sg.Subject)
                                         .FirstOrDefault(filter);
            return Mapper.Map<GroupVm>(entity);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filter = null)
        {
            var query = DbContext.Groups.Include(g => g.Students)
                                        .Include(g => g.SubjectGroups)
                                        .ThenInclude(sg => sg.Subject)
                                        .AsQueryable();
            if (filter is not null)
                query = query.Where(filter);

            return Mapper.Map<IEnumerable<GroupVm>>(query);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId)
                          ?? throw new Exception("Nie znaleziono studenta.");

            student.GroupId = vm.GroupId;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId)
                          ?? throw new Exception("Nie znaleziono studenta.");

            student.GroupId = null;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            DbContext.SubjectGroups.Add(new SubjectGroup { GroupId = vm.GroupId, SubjectId = vm.SubjectId });
            DbContext.SaveChanges();
            return GetGroup(g => g.Id == vm.GroupId);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
        {
            var link = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == vm.GroupId && sg.SubjectId == vm.SubjectId);
            if (link is not null)
                DbContext.SubjectGroups.Remove(link);

            DbContext.SaveChanges();
            return GetGroup(g => g.Id == vm.GroupId);
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == vm.SubjectId)
                          ?? throw new Exception("Nie znaleziono przedmiotu.");

            subject.TeacherId = vm.TeacherId;
            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == vm.SubjectId)
                          ?? throw new Exception("Nie znaleziono przedmiotu.");

            subject.TeacherId = null;
            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
    }
}
