using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class SubjectService : ISubjectService
    {
        public SubjectService()
        {
        }

        public SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm)
        {
            throw new NotImplementedException();
        }

        public SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null)
        {
            throw new NotImplementedException();
        }
    }
}