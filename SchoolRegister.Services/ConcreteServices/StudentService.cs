using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class StudentService : BaseService, IStudentService
{
    public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        : base(dbContext, mapper, logger) {}
    public StudentVm GetStudent(Expression<Func<Student, bool>> filterExpression)
    {
        try
        {
            if (filterExpression == null)
                throw new ArgumentNullException("Filter expression is null");
            var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(filterExpression);
            return Mapper.Map<StudentVm>(studentEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filterExpression = null)
    {
        try
        {
            var studentEntities = DbContext.Users.OfType<Student>().AsQueryable();
            if (filterExpression != null)
                studentEntities = studentEntities.Where(filterExpression);

            return Mapper.Map<IEnumerable<StudentVm>>(studentEntities);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}