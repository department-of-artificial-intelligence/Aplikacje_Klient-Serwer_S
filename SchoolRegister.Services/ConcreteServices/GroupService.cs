using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

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
            var group = Mapper.Map<Group>(addOrUpdateGroupVm);
            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0) DbContext.Groups.Add(group);
            else DbContext.Groups.Update(group);
            
            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(group);
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            var group = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
        {
            var query = DbContext.Groups.AsQueryable();
            if (filterPredicate != null) query = query.Where(filterPredicate);
            return Mapper.Map<IEnumerable<GroupVm>>(query.ToList());
        }

        // TODO
        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm) => throw new NotImplementedException();
        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm) => throw new NotImplementedException();
        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm) => throw new NotImplementedException();
        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm) => throw new NotImplementedException();
        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectGroupVm) => throw new NotImplementedException();
        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm) => throw new NotImplementedException();
    }
}