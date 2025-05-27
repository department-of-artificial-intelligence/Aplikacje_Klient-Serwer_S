using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudenttoGroupVm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachDetachSubjectVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null);
    }
}