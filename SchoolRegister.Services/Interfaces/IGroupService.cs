using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels; 
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterExpression = null);
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm);
    }
}