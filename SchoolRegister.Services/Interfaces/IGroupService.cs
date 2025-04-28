using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.Interfaces
{
    public interface IGroupService
    {
        GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm);
        GroupVm GetGroup(Expression<Func<Group, bool>>? filterExpression);
        IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterExpression = null);
        StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm);
        StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm);
        GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm);
        GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm);
        SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm);
        SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm);
    }
}