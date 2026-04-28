using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface ISubjectService
    {
        SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm add_or_update_vm);
        SubjectVm GetSubject(Expression<Func<Subject, bool>> filter_expression);
        IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filter_expression = null);
        bool RemoveSubject(Expression<Func<Subject, bool>> filterExpression);
    }
}