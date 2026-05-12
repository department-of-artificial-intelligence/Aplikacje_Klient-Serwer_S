using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;
namespace SchoolRegister.ViewModels.VM
{
    public interface ISubjectService
    {
        SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm);
        SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression);
        IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null!);
        bool RemoveSubject(Expression<Func<Subject, bool>> filterExpression);
    }
}
