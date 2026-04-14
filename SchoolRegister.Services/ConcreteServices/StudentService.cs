using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger<StudentService> logger) 
            : base(dbContext, mapper, logger) { }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(filterPredicate);
            return Mapper.Map<StudentVm>(student);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
        {
            var query = DbContext.Users.OfType<Student>().AsQueryable();
            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }
            return Mapper.Map<IEnumerable<StudentVm>>(query.ToList());
        }
    }
}