using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm add_or_update_group_vm);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attach_student_to_group_vm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attach_subject_group_vm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attach_subject_to_teacher_vm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detach_student_to_group_vm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detach_subject_group_vm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detach_subject_to_teacher_vm);
        GroupVm GetGroup(Expression<Func<Group, bool>> filter_predicate);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filter_predicate = null);
        void AddStudentToGroup(int studentId, int groupId);
        void RemoveStudentFromGroup(int studentId, int groupId);
    }
}