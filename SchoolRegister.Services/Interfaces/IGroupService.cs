using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm vm);
        GroupVm GetGroup(Expression<Func<Group, bool>> filterExpression);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterExpression = null);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm);
    }
}
