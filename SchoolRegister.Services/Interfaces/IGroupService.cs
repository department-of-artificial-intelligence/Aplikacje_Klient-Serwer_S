using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        
      
        GroupVm GetGroup(Expression<Func<SchoolRegister.Model.DataModels.Group, bool>> filterPredicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<SchoolRegister.Model.DataModels.Group, bool>> filterPredicate = null);
    }
}