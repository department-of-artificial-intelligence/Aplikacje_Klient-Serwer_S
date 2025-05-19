using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>>? filterExpression)
        {
            var student = DbContext.Users.OfType<Student>()
                .Include(s => s.Grades)
                .Include(s => s.Group)
                .Include(s => s.Parent)
                .FirstOrDefault(filterExpression);

            return Mapper.Map<StudentVm>(student);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filterExpression = null)
        {
            var query = DbContext.Users.OfType<Student>()
                .Include(s => s.Grades)
                .Include(s => s.Group)
                .Include(s => s.Parent)
                .AsQueryable();

            if (filterExpression != null)
                query = query.Where(filterExpression);

            return Mapper.Map<IEnumerable<StudentVm>>(query);
        }
    }
}
