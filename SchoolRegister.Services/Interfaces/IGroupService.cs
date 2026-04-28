using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm GetGroup(Expression<Func<Group, bool>> filter);

        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filter = null);
    }
}