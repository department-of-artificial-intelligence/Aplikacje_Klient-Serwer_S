using System.Linq.Expressions;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroup);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm attachDetachSubjectGroupVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectGroupVm attachDetachSubjectVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        GroupVm GetGroup(Expression<Func<Group,bool>> filterPredicate);
        IEnumerable<Group> GetGroups(Expression<Func<Group,bool>> filterPredicate = null);
    }
}
