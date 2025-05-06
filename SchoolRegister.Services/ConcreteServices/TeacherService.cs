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
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public TeacherVm AddOrUpdateTeacher(AddOrUpdateTeacherVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var teacherEntity = Mapper.Map<Teacher>(addOrUpdateVm);

                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Teachers.Add(teacherEntity);  
                else
                    DbContext.Teachers.Update(teacherEntity);  

                DbContext.SaveChanges();

                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);
                return teacherVm;
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");

                var teacherEntity = DbContext.Teachers.FirstOrDefault(filterExpression);  
                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);
                return teacherVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterExpression = null)
        {
            try
            {
                var teacherEntities = DbContext.Teachers.AsQueryable();  // Używamy DbSet<Teacher>

                if (filterExpression != null)
                    teacherEntities = teacherEntities.Where(filterExpression);

                var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
                return teacherVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
