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

        public bool AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            try
            {
                var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId);
                if (student == null) return false;
                student.GroupId = vm.GroupId;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public bool DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            try
            {
                var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId);
                if (student == null) return false;
                student.GroupId = null;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}