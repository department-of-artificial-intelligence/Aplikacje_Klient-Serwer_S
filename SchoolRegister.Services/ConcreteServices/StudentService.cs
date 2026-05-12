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
using Microsoft.EntityFrameworkCore;

namespace SchoolRegister.Services.ConcreteServices
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public StudentVm? GetStudent(Expression<Func<Student, bool>> filter)
        {
            try
            {
                var entity = DbContext.Users
                    .OfType<Student>()
                    .Include(s => s.Parent)
                    .Include(s => s.Group)
                    .FirstOrDefault(filter);

                if (entity == null)
                    return null;

                return Mapper.Map<StudentVm>(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filter = null)
        {
            try
            {
                var entities = DbContext.Users
                    .OfType<Student>()
                    .Include(s => s.Parent)
                    .Include(s => s.Group)
                    .AsQueryable();

                if (filter != null)
                    entities = entities.Where(filter);

                return Mapper.Map<IEnumerable<StudentVm>>(entities);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}