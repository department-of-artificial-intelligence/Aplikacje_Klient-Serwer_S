using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;

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
            var group = DbContext.Set<SubjectGroup>().Find(addOrUpdateGroupVm.Id);
            if (group == null)
            {
                group = Mapper.Map<SubjectGroup>(addOrUpdateGroupVm);
                DbContext.Set<SubjectGroup>().Add(group);
            }
            else
            {
                Mapper.Map(addOrUpdateGroupVm, group);
                DbContext.Set<SubjectGroup>().Update(group);
            }
            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(group);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            var student = DbContext.Set<Student>().Find(attachStudentToGroupVm.StudentId);
            var group = DbContext.Set<SubjectGroup>().Find(attachStudentToGroupVm.GroupId);
            

            if(student != null && group != null)
            {

                group.Students.Add(student); 
                DbContext.SaveChanges();
            }
            
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            var group = DbContext.Set<SubjectGroup>().Find(attachSubjectGroupVm.GroupId);
            if(group != null)
            {
                group.SubjectId = attachSubjectGroupVm.SubjectId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<GroupVm>(group);
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            var subject = DbContext.Set<Subject>().Find(attachDetachSubjectToTeacherVm.SubjectId);
            if(subject != null)
            {
                subject.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            var student = DbContext.Set<Student>().Find(detachStudentToGroupVm.StudentId);
            var group = DbContext.Set<SubjectGroup>().Find(detachStudentToGroupVm.GroupId);
            
            if(student != null && group != null)
            {
                group.Students.Remove(student); 
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm)
        {
            var group = DbContext.Set<SubjectGroup>().Find(detachSubjectVm.GroupId);
            if (group != null)
            {
             
                group.SubjectId = 0; 
                DbContext.SaveChanges();
            }
            return Mapper.Map<GroupVm>(group);
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            var subject = DbContext.Set<Subject>().Find(attachDetachSubjectToTeacherVm.SubjectId);
            if (subject != null)
            {
             
                subject.TeacherId = null; 
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }

        public GroupVm GetGroup(Expression<Func<SubjectGroup, bool>> filterPredicate)
        {
            var group = DbContext.Set<SubjectGroup>().FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<SubjectGroup, bool>> filterPredicate = null)
        {
            var query = DbContext.Set<SubjectGroup>().AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<GroupVm>>(query.ToList());
        }
    }
    }
}