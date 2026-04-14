using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filter)
        {
            var entity = DbContext.Users.OfType<Student>().FirstOrDefault(filter);
            return Mapper.Map<StudentVm>(entity);
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filter = null)
        {
            var query = DbContext.Users.OfType<Student>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return Mapper.Map<IEnumerable<StudentVm>>(query);
        }
    }
}