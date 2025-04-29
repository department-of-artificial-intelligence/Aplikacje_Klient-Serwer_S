using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdaTeGroup(AddOrUpdateGroupVM addOrUpdateGroupVM);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroup attachSubjectGroupVm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroup detachDetachSubjectGroupVm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        GroupVm GetGroup(Expression<Func<Group,bool>>filterPredicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group,bool>>?filterPredicate=null);
    }
}