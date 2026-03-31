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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext db_context, IMapper mapper, ILogger logger)
            : base(db_context, mapper, logger) { }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filter_predicate)
        {
            var student_entity = DbContext.Users.OfType<Student>().FirstOrDefault(filter_predicate);
            return Mapper.Map<StudentVm>(student_entity);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filter_predicate = null)
        {
            var student_entities = DbContext.Users.OfType<Student>().AsQueryable();
            if (filter_predicate != null)
                student_entities = student_entities.Where(filter_predicate);

            return Mapper.Map<IEnumerable<StudentVm>>(student_entities);
        }
    }
}