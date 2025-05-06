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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public StudentVm AddOrUpdateStudent(AddOrUpdateStudentVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var studentEntity = Mapper.Map<Student>(addOrUpdateVm);

                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Students.Add(studentEntity);
                else
                    DbContext.Students.Update(studentEntity);

                DbContext.SaveChanges();

                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");

                var studentEntity = DbContext.Students.FirstOrDefault(filterExpression);
                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterExpression = null)
        {
            try
            {
                var studentEntities = DbContext.Students.AsQueryable();

                if (filterExpression != null)
                    studentEntities = studentEntities.Where(filterExpression);

                var studentVms = Mapper.Map<IEnumerable<StudentVm>>(studentEntities);
                return studentVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
