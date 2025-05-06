using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateVm);
        GroupVm GetGroup(Expression<Func<Group, bool>> filterExpression);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterExpression = null);
        IEnumerable<GroupVm> GetGroupsWithSubjects(Expression<Func<Group, bool>> filterExpression = null);
    }
}
