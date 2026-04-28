using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly UserManager<User> _userManager;
        
        public GradeService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger logger,
            UserManager<User> userManager) : base (dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        
        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            var grade = new Grade
            {
                StudentId = addGradeToStudentVm.StudentId,
              
                GradeValue = addGradeToStudentVm.GradeValue,
                SubjectId = addGradeToStudentVm.SubjectId
            };
            
            DbContext.Set<Grade>().Add(grade);
            DbContext.SaveChanges();
            
            return Mapper.Map<GradeVm>(grade);
        }
        
        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            var grades = DbContext.Set<Grade>()
                .Where(g => g.StudentId == getGradesVm.StudentId)
                .ToList();

            return new GradesReportVm
            {
                StudentId = getGradesVm.StudentId,
            
                AverageGrade = grades.Any() ? grades.Average(g => (int)g.GradeValue) : 0,
                Grades = Mapper.Map<System.Collections.Generic.List<GradeVm>>(grades)
            };
        }
    }
}