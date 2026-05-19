using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            :base(dbContext, mapper, logger) {}
        public SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(addOrUpdateVm);

                var subjectEntity = Mapper.Map<Subject>(addOrUpdateVm);

                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Subjects.Add(subjectEntity);
                else
                    DbContext.Subjects.Update(subjectEntity);

                DbContext.SaveChanges();

                return Mapper.Map<SubjectVm>(subjectEntity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in AddOrUpdateSubject");
                throw;
            }
        }
        public SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(filterExpression);

                var subjectEntity = DbContext.Subjects
                    .Include(s => s.SubjectGroups)
                        .ThenInclude(sg => sg.Group)
                    .Include(s => s.Teacher)
                    .FirstOrDefault(filterExpression);

                return Mapper.Map<SubjectVm>(subjectEntity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetSubject");
                throw;
            }
        }
        public IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>>? filterExpression = null)
        {
            try
            {
                var subjectEntities = DbContext.Subjects
                    .Include(s => s.SubjectGroups)
                        .ThenInclude(sg => sg.Group)
                    .Include(s => s.Teacher)
                    .AsQueryable();

                if (filterExpression != null)
                    subjectEntities = subjectEntities.Where(filterExpression);

                return Mapper.Map<IEnumerable<SubjectVm>>(subjectEntities.ToList());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in GetSubjects");
                throw;
            }
        }

        public bool RemoveSubject(Expression<Func<Subject, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }
    }
}