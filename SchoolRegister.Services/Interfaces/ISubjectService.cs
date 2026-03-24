using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;
namespace SchoolRegister.ViewModels.VM;
{
    public interface ISubjectService
{
    SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm);
    SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression);
    IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null);
}
}
