using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface ISubjectService
    {
        Subject AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm);
        SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression);
        IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null);
    }
}