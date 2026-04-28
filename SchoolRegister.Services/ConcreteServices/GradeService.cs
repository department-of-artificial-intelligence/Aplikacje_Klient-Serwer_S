using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
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

        public IEnumerable<GradeVm> GetGrades(Expression<Func<Grade, bool>>? filter = null)
        {
            try
            {
                var entities = DbContext.Grades.AsQueryable();

                if (filter != null)
                    entities = entities.Where(filter);

                return Mapper.Map<IEnumerable<GradeVm>>(entities);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while getting grades");
                throw;
            }
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm vm)
        {
            try
            {
                var grade = new Grade
                {
                    StudentId = vm.StudentId,
                    SubjectId = vm.SubjectId,
                    DateOfIssue = DateTime.Now,
                    GradeValue = vm.GradeValue
                };

                DbContext.Grades.Add(grade);
                DbContext.SaveChanges();

                return Mapper.Map<GradeVm>(grade);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while adding grade");
                throw;
            }
        }

        public IEnumerable<GradeVm> GetGradesReportForStudent(GetGradesReportVm vm)
        {
            try
            {
                var grades = DbContext.Grades
                    .Where(g => g.StudentId == vm.StudentId)
                    .ToList();

                return Mapper.Map<IEnumerable<GradeVm>>(grades);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while getting grades report");
                throw;
            }
        }
    }
}