using System;
using System.Collections.Generic;
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
        private UserManager<User> _userManager;

        public GroupService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger logger,
            UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if(addOrUpdateGroupVm == null)
                    throw new ArgumentNullException($"addOrUpdateGroupVm paramater is null");

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

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup)
        {
            try
            {
                if(attachDetachStudentToGroup == null)
                    throw new ArgumentNullException($"attachDetachStudentToGroup paramater is null");

                var group = DbContext.Groups.FirstOrDefault(g => attachDetachStudentToGroup.GroupId == g.Id);
                var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => attachDetachStudentToGroup.StudentId == s.Id);

                if(group == null)
                {
                    throw new InvalidOperationException(
                        $"No group with id {attachDetachStudentToGroup.GroupId} found"
                    );
                }

                if(student == null)
                {
                    throw new InvalidOperationException(
                        $"No student with id {attachDetachStudentToGroup.StudentId} found"
                    );
                }

                student.GroupId = group.Id;

                DbContext.SaveChanges();
                
                var studentVm = Mapper.Map<StudentVm>(student);
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
                if(attachSubjectGroupVm == null)
                    throw new ArgumentNullException($"attachSubjectGroupVm paramater is null");

                var group = DbContext.Groups
                    .FirstOrDefault(g => attachSubjectGroupVm.GroupId == g.Id);

                var subject = DbContext.Subjects.FirstOrDefault(s => attachSubjectGroupVm.SubjectId == s.Id);

                if(group == null)
                {
                    throw new InvalidOperationException(
                        $"No group with id {attachSubjectGroupVm.GroupId} found"
                    );
                }

                if(subject == null)
                {
                    throw new InvalidOperationException(
                        $"No subject with id {attachSubjectGroupVm.SubjectId} found"
                    );
                }


                DbContext.SubjectGroups.Add(new SubjectGroup()
                {
                    SubjectId = subject.Id,
                    GroupId = group.Id
                });
                
                DbContext.SaveChanges();


                
                var groupVm = Mapper.Map<GroupVm>(group);
                return groupVm;
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
                if(attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"attachDetachSubjectToTeacherVm paramater is null");

                var teacher = DbContext.Users.OfType<Teacher>()
                    .FirstOrDefault(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);

                var subject = DbContext.Subjects
                    .FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);

                if(teacher == null)
                {
                    throw new InvalidOperationException(
                        $"No teacher with id {attachDetachSubjectToTeacherVm.TeacherId} found"
                    );
                }

                if(subject == null)
                {
                    throw new InvalidOperationException(
                        $"No subject with id {attachDetachSubjectToTeacherVm.SubjectId} found"
                    );
                }

                subject.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                DbContext.SaveChanges();

                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup)
        {
            try
            {
                if(attachDetachStudentToGroup == null)
                    throw new ArgumentNullException($"addOrUpdateGroupVm paramater is null");

                var group = DbContext.Groups.FirstOrDefault(g=> attachDetachStudentToGroup.GroupId == g.Id);
                var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => attachDetachStudentToGroup.StudentId == s.Id);

                if(group == null)
                {
                    throw new InvalidOperationException(
                        $"No group found with id {attachDetachStudentToGroup.GroupId}"
                    );
                }

                if(student == null)
                {
                    throw new InvalidOperationException(
                        $"No student found with id {attachDetachStudentToGroup.StudentId}"
                    );
                }

                student.GroupId = null;
                
                var studentVm = Mapper.Map<StudentVm>(student);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            } 
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm attachDetachSubjectGroupVm)
        {
            try
            {
                if(attachDetachSubjectGroupVm == null)
                    throw new ArgumentNullException($"attachSubjectGroupVm paramater is null");

                var group = DbContext.Groups
                    .FirstOrDefault(g => attachDetachSubjectGroupVm.GroupId == g.Id);

                var subject = DbContext.Subjects.FirstOrDefault(s => attachDetachSubjectGroupVm.SubjectId == s.Id);

                if(group == null)
                {
                    throw new InvalidOperationException(
                        $"No group found with id {attachDetachSubjectGroupVm.GroupId}"
                    );
                }

                if(subject == null)
                {
                    throw new InvalidOperationException(
                        $"No subject found with id {attachDetachSubjectGroupVm.SubjectId}"
                    );
                }
                
                DbContext.SubjectGroups.Remove(
                    DbContext.SubjectGroups.FirstOrDefault(g => g.SubjectId == attachDetachSubjectGroupVm.SubjectId)
                );

                
                DbContext.SaveChanges();

                var groupVm = Mapper.Map<GroupVm>(group);
                return groupVm;
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
                if(attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"attachDetachSubjectToTeacherVm paramater is null");

                var teacher = DbContext.Users.OfType<Teacher>()
                    .FirstOrDefault(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);

                var subject = DbContext.Subjects
                    .FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);

                if(teacher == null)
                {
                    throw new InvalidOperationException(
                        $"No teacher with id {attachDetachSubjectToTeacherVm.TeacherId} found"
                    );
                }

                if(subject == null)
                {
                    throw new InvalidOperationException(
                        $"No subject with id {attachDetachSubjectToTeacherVm.SubjectId} found"
                    );
                }

                subject.TeacherId = null;
                DbContext.SaveChanges();

                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm GetGroup(Expression<Func<Group,bool>> filterPredicate)
        {
            
            var group = DbContext
                    .Groups
                    .FirstOrDefault(filterPredicate);

            
            var groupEntity = Mapper.Map<GroupVm>(group);
            return groupEntity;
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group,bool>> filterPredicate = null)
        {
            if(filterPredicate == null)
                filterPredicate = (x) => true;
            
            var groups = DbContext.Groups.Where(filterPredicate);
            var groupsVm = new List<GroupVm>();

            foreach (var g in groups)
            {
                groupsVm.Add(Mapper.Map<GroupVm>(g));
            }
            
            return groupsVm;
        }
    }


}