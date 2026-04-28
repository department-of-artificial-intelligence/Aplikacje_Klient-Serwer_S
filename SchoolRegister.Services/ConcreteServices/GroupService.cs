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
            var group = DbContext.Set<SchoolRegister.Model.DataModels.Group>().Find(addOrUpdateGroupVm.Id);
            if (group == null)
            {
                group = Mapper.Map<SchoolRegister.Model.DataModels.Group>(addOrUpdateGroupVm);
                DbContext.Set<SchoolRegister.Model.DataModels.Group>().Add(group);
            }
            else
            {
                Mapper.Map(addOrUpdateGroupVm, group);
                DbContext.Set<SchoolRegister.Model.DataModels.Group>().Update(group);
            }
            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(group);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            var student = DbContext.Set<Student>().Find(attachStudentToGroupVm.StudentId);
            if(student != null)
            {
                student.GroupId = attachStudentToGroupVm.GroupId; 
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            var exists = DbContext.Set<SubjectGroup>().Any(sg => 
                sg.SubjectId == attachSubjectGroupVm.SubjectId && 
                sg.GroupId == attachSubjectGroupVm.GroupId);

            if (!exists)
            {
                var subjectGroup = new SubjectGroup 
                {
                    SubjectId = attachSubjectGroupVm.SubjectId,
                    GroupId = attachSubjectGroupVm.GroupId
                };
                DbContext.Set<SubjectGroup>().Add(subjectGroup);
                DbContext.SaveChanges();
            }

            var group = DbContext.Set<SchoolRegister.Model.DataModels.Group>().Find(attachSubjectGroupVm.GroupId);
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
            if(student != null)
            {
                student.GroupId = null; 
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm)
        {
            var subjectGroup = DbContext.Set<SubjectGroup>().FirstOrDefault(sg => 
                sg.SubjectId == detachSubjectVm.SubjectId && 
                sg.GroupId == detachSubjectVm.GroupId);
            
            if (subjectGroup != null)
            {
                DbContext.Set<SubjectGroup>().Remove(subjectGroup); 
                DbContext.SaveChanges();
            }

            var group = DbContext.Set<SchoolRegister.Model.DataModels.Group>().Find(detachSubjectVm.GroupId);
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

       
        public GroupVm GetGroup(Expression<Func<SchoolRegister.Model.DataModels.Group, bool>> filterPredicate)
        {
            var group = DbContext.Set<SchoolRegister.Model.DataModels.Group>().FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<SchoolRegister.Model.DataModels.Group, bool>> filterPredicate = null)
        {
            var query = DbContext.Set<SchoolRegister.Model.DataModels.Group>().AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<GroupVm>>(query.ToList());
        }
    }
}