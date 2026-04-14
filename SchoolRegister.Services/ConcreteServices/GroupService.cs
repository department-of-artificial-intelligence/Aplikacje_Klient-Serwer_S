using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

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
        throw new NotImplementedException();
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        throw new NotImplementedException();
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectToGroup)
    {
        throw new NotImplementedException();
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        throw new NotImplementedException();
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
    {
        throw new NotImplementedException();
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachDetachSubject)
    {
        throw new NotImplementedException();
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        throw new NotImplementedException();
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
    {
        try
        {
            if (filterPredicate == null)
                throw new ArgumentNullException(nameof(filterPredicate));

            var groupEntity = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(groupEntity);
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
            var groups = DbContext.Groups.AsQueryable();

            if (filterPredicate != null)
                groups = groups.Where(filterPredicate);

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}