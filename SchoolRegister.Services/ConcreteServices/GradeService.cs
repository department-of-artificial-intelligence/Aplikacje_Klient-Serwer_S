using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) { }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm vm)
        {
            var teacher = DbContext.Users.OfType<Teacher>()
                .FirstOrDefault(t => t.Id == vm.TeacherId);

            if (teacher == null)
                throw new Exception("Not a teacher");

            var grade = new Grade
            {
                StudentId = vm.StudentId,
                SubjectId = vm.SubjectId,
                GradeValue = (int)vm.GradeValue,
                DateOfIssue = DateTime.Now,
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }

        public object GetGradesReportForStudent(GetGradesReportVm vm)
        {
            var user = DbContext.Users.FirstOrDefault(u => u.Id == vm.GetterUserId);
            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(s => s.Id == vm.StudentId);

            if (user is Teacher) { }
            else if (user is Student && user.Id == student.Id) { }
            else if (user is Parent parent && student.ParentId == parent.Id) { }
            else throw new Exception("Access denied");

            return DbContext.Grades
                .Where(g => g.StudentId == vm.StudentId)
                .ToList();
        }
    }
}