using System.Linq.Expressions;
using System.Text.RegularExpressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm GetGroup(Expression<Func<Group, bool>> filter);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filter = null);
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm vm);

        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm);

        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm);

        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm);
    }
}