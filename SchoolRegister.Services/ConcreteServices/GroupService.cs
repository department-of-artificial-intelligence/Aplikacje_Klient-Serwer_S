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
            return null;
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup)
        {
            return null;
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            return null;
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            return null;
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup)
        {
            return null;
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm attachDetachSubjectGroupVm)
        {
            return null;
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectGroupVm attachDetachSubjectVm)
        {
            return null;
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            return null;
        }

        public GroupVm GetGroup(Expression<Func<Group,bool>> filterPredicate)
        {
            
            var group = DbContext
                    .Groups
                    .FirstOrDefault(filterPredicate);

            
            var groupEntity = Mapper.Map<GroupVm>(group);
            return groupEntity;
        }

        public IEnumerable<Group> GetGroups(Expression<Func<Group,bool>> filterPredicate = null)
        {
            if(filterPredicate == null)
                filterPredicate = (x) => true;
            return DbContext.Groups.Where(filterPredicate);
        }
    }


}