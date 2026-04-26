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
            try
            {
                if (addOrUpdateGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);

                if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                    DbContext.Groups.Add(groupEntity);
                else
                    DbContext.Groups.Update(groupEntity);

                DbContext.SaveChanges();

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            try
            {
                if (attachStudentToGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);
                if (student != null)
                {
                    student.GroupId = attachStudentToGroupVm.GroupId;
                    DbContext.SaveChanges();
                }

                var studentWithGroup = DbContext.Users.OfType<Student>().Include(s => s.Group).FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);
                var studentVm = Mapper.Map<StudentVm>(studentWithGroup);

                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            try
            {
                if (attachSubjectGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var exists = DbContext.SubjectGroups.Any(sg => sg.GroupId == attachSubjectGroupVm.GroupId && sg.SubjectId == attachSubjectGroupVm.SubjectId);
                if (!exists)
                {
                    DbContext.SubjectGroups.Add(new SubjectGroup { GroupId = attachSubjectGroupVm.GroupId, SubjectId = attachSubjectGroupVm.SubjectId });
                    DbContext.SaveChanges();
                }

                return GetGroup(g => g.Id == attachSubjectGroupVm.GroupId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
                if (subject != null)
                {
                    subject.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                    DbContext.SaveChanges();
                }

                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            try
            {
                if (detachStudentToGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var student = DbContext.Users.OfType<Student>().Include(s => s.Group).FirstOrDefault(s => s.Id == detachStudentToGroupVm.StudentId);

                if (student != null)
                {
                    student.GroupId = null;
                    student.Group = null;
                    DbContext.SaveChanges();
                }

                var studentVm = Mapper.Map<StudentVm>(student);
                return studentVm ?? new StudentVm(); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm)
        {
            try
            {
                if (detachSubjectVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var subjectGroup = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == detachSubjectVm.GroupId && sg.SubjectId == detachSubjectVm.SubjectId);
                if (subjectGroup != null)
                {
                    DbContext.SubjectGroups.Remove(subjectGroup);
                    DbContext.SaveChanges();
                }

                return GetGroup(g => g.Id == detachSubjectVm.GroupId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId && s.TeacherId == attachDetachSubjectToTeacherVm.TeacherId);
                if (subject != null)
                {
                    subject.TeacherId = null;
                    DbContext.SaveChanges();
                }

                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException($"FilterPredicate is null");

                var groupEntity = DbContext.Groups
                    .Include(g => g.Students)
                    .Include(g => g.SubjectGroups).ThenInclude(sg => sg.Subject)
                    .FirstOrDefault(filterPredicate);

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
        {
            try
            {
                var groupEntities = DbContext.Groups.AsQueryable();

                if (filterPredicate != null)
                    groupEntities = groupEntities.Where(filterPredicate);

                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
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