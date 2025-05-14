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
            : base(dbContext, mapper, logger) { }

        public StudentVm GetStudent(Expression<Func<Student,bool>> filterPredicate)
        {
            var student = DbContext
                    .Users.OfType<Student>()
                    .FirstOrDefault(filterPredicate);

            
            var studentEntity = Mapper.Map<StudentVm>(student);
            return studentEntity;
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student,bool>> filterPredicate = null)
        {
            if(filterPredicate == null)
                filterPredicate = (x) => true;
            
            var students = DbContext.Users.OfType<Student>().Where(filterPredicate);
            var studentsVm = new List<StudentVm>();

            foreach (var s in students)
            {
                studentsVm.Add(Mapper.Map<StudentVm>(s));
            }
            
            return studentsVm;
        }
    }
}
