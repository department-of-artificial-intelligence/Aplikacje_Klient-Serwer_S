using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces;

public interface IGroupService
{
    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
    StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm);

    GroupVm AttachSubjectToGroup(AttachDetachSubjectToGroupVm attachSubjectToGroupVm);

    SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachSubjectToTeacherVm);

    StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm);

    GroupVm DetachSubjectFromGroup(AttachDetachSubjectToGroupVm detachSubjectGroupVm);

    SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachSubjectToTeacherVm);

    GroupVm GetGroup(Expression<Func<Group, bool>> filterExpression);
    IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterExpression = null);


}