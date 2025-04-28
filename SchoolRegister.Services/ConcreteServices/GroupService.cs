using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            throw new NotImplementedException();
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>>? filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterExpression = null)
        {
            throw new NotImplementedException();
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            throw new NotImplementedException();
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            throw new NotImplementedException();
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            throw new NotImplementedException();
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
        {
            throw new NotImplementedException();
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm)
        {
            throw new NotImplementedException();
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm)
        {
            throw new NotImplementedException();
        }
    }
}