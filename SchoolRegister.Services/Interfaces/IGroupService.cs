using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup();
        StudentVm AttachStudentToGroup();
        GroupVm AttachSubjectToGroup();
        SubjectVm AttachTeacherToSubject();
        StudentVm DetachStudentFromGroup();
        GroupVm DetachSubjectFromGroup();
        SubjectVm DetachTeacherFromSubject();
        GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null);

    }
}