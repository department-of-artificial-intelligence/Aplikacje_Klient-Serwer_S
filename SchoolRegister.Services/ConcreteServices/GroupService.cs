using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        : BaseService(dbContext, mapper, logger), IGroupService
    {
        public GroupVm? GetGroup(Expression<Func<Group, bool>> filter)
        {
            try
            {
                var entity = DbContext.Groups.FirstOrDefault(filter);

                if (entity == null)
                    return null;

                return Mapper.Map<GroupVm>(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetGroup");
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filter = null)
        {
            try
            {
                var entities = DbContext.Groups.AsQueryable();

                if (filter != null)
                    entities = entities.Where(filter);

                return Mapper.Map<IEnumerable<GroupVm>>(entities);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetGroups");
                throw;
            }
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm vm)
        {
            try
            {
                var entity = new Group
                {
                    Id = vm.Id ?? 0,
                    Name = vm.Name
                };

                if (!vm.Id.HasValue || vm.Id == 0)
                    DbContext.Groups.Add(entity);
                else
                    DbContext.Groups.Update(entity);

                DbContext.SaveChanges();

                return Mapper.Map<GroupVm>(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in AddOrUpdateGroup");
                throw;
            }
        }

        public GroupVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>().First(s => s.Id == vm.StudentId);
            student.GroupId = vm.GroupId;
            DbContext.SaveChanges();

            var group = DbContext.Groups.First(g => g.Id == vm.GroupId);
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Users.OfType<Student>().First(s => s.Id == vm.StudentId);
            student.GroupId = null;
            DbContext.SaveChanges();

            var group = DbContext.Groups.First(g => g.Id == vm.GroupId);
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            var sg = new SubjectGroup
            {
                SubjectId = vm.SubjectId,
                GroupId = vm.GroupId
            };

            DbContext.SubjectGroups.Add(sg);
            DbContext.SaveChanges();

            var group = DbContext.Groups.First(g => g.Id == vm.GroupId);
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
        {
            var sg = DbContext.SubjectGroups
                .First(x => x.SubjectId == vm.SubjectId && x.GroupId == vm.GroupId);

            DbContext.SubjectGroups.Remove(sg);
            DbContext.SaveChanges();

            var group = DbContext.Groups.First(g => g.Id == vm.GroupId);
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.First(s => s.Id == vm.SubjectId);
            subject.TeacherId = vm.TeacherId;
            DbContext.SaveChanges();

            var group = DbContext.SubjectGroups
                .Where(sg => sg.SubjectId == vm.SubjectId)
                .Select(sg => sg.Group)
                .First();

            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm)
        {
            var subject = DbContext.Subjects.First(s => s.Id == vm.SubjectId);
            subject.TeacherId = null;
            DbContext.SaveChanges();

            var group = DbContext.SubjectGroups
                .Where(sg => sg.SubjectId == vm.SubjectId)
                .Select(sg => sg.Group)
                .First();

            return Mapper.Map<GroupVm>(group);
        }
    }
}