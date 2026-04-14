using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public GroupVm AddOrUpdateGroup()
        {
            throw new NotImplementedException();
        }

        public StudentVm AttachStudentToGroup()
        {
            throw new NotImplementedException();
        }

        public GroupVm AttachSubjectToGroup()
        {
            throw new NotImplementedException();
        }

        public SubjectVm AttachTeacherToSubject()
        {
            throw new NotImplementedException();
        }

        public StudentVm DetachStudentFromGroup()
        {
            throw new NotImplementedException();
        }

        public GroupVm DetachSubjectFromGroup()
        {
            throw new NotImplementedException();
        }

        public SubjectVm DetachTeacherFromSubject()
        {
            throw new NotImplementedException();
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
        {
            throw new NotImplementedException();
        }
    }
}