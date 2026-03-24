using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.ConcreteServices;
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger logger)
            : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            var student = _dbContext.Students
                .AsQueryable()
                .FirstOrDefault(filterPredicate);

            if (student == null)
            {
                _logger.LogWarning("Student not found");
                return null;
            }

            return _mapper.Map(student);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
        {
            var query = _dbContext.Students.AsQueryable();

            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }

            return query
                .Select(s => _mapper.Map(s))
                .ToList();
        }
    }
