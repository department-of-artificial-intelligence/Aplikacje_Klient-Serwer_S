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
    public class GradeService : BaseService, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public GradeVm AddOrUpdateGrade(AddOrUpdateGradeVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var gradeEntity = Mapper.Map<Grade>(addOrUpdateVm);

                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Grades.Add(gradeEntity);
                else
                    DbContext.Grades.Update(gradeEntity);

                DbContext.SaveChanges();

                var gradeVm = Mapper.Map<GradeVm>(gradeEntity);
                return gradeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradeVm GetGrade(Expression<Func<Grade, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");

                var gradeEntity = DbContext.Grades.FirstOrDefault(filterExpression);
                var gradeVm = Mapper.Map<GradeVm>(gradeEntity);
                return gradeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GradeVm> GetGrades(Expression<Func<Grade, bool>> filterExpression = null)
        {
            try
            {
                var gradeEntities = DbContext.Grades.AsQueryable();

                if (filterExpression != null)
                    gradeEntities = gradeEntities.Where(filterExpression);

                var gradeVms = Mapper.Map<IEnumerable<GradeVm>>(gradeEntities);
                return gradeVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GradeVm> GetGradesWithSubjectAndStudent(Expression<Func<Grade, bool>> filterExpression = null)
        {
            try
            {
                var gradeEntities = DbContext.Grades
                    .Include(g => g.Subject)
                    .Include(g => g.Student)
                    .AsQueryable();

                if (filterExpression != null)
                    gradeEntities = gradeEntities.Where(filterExpression);

                var gradeVms = Mapper.Map<IEnumerable<GradeVm>>(gradeEntities);
                return gradeVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
