using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Services.ConcreteServices
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            ILogger<StudentService> logger) 
            : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            var student = DbContext.Set<Student>().FirstOrDefault(filterPredicate);
            return Mapper.Map<StudentVm>(student);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
        {
            var query = DbContext.Set<Student>().AsQueryable();
            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }
            return Mapper.Map<IEnumerable<StudentVm>>(query.ToList());
        }

        public void AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Set<Student>().FirstOrDefault(s => s.Id == vm.StudentId);
            var group = DbContext.Set<Group>().FirstOrDefault(g => g.Id == vm.GroupId);

            if (student != null && group != null)
            {
                student.GroupId = group.Id;
                DbContext.SaveChanges();
            }
        }

        public void DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            var student = DbContext.Set<Student>().FirstOrDefault(s => s.Id == vm.StudentId);

            if (student != null && student.GroupId == vm.GroupId)
            {
                student.GroupId = null;
                DbContext.SaveChanges();
            }
        }
    }
}