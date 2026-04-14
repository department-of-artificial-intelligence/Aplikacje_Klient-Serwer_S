using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Interfaces
{
    public interface ISubjectService
    {
        SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm vm);
        SubjectVm GetSubject(Expression<Func<Subject, bool>> filter);
        IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filter = null);
    }
}