using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filter)
        {
            var entity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filter);
            return Mapper.Map<TeacherVm>(entity);
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filter = null)
        {
            var query = DbContext.Users.OfType<Teacher>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return Mapper.Map<IEnumerable<TeacherVm>>(query);
        }

        public IEnumerable<string> GetTeachersGroups(TeachersGroupsVm vm)
        {
            var subjects = DbContext.Subjects
                .Where(s => s.TeacherId == vm.TeacherId)
                .Select(s => s.Id)
                .ToList();

            var groups = DbContext.SubjectGroups
                .Where(sg => subjects.Contains(sg.SubjectId))
                .Select(sg => sg.Group.Name)
                .ToList();

            return groups;
        }
    }
}