using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Linq;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly UserManager<User> _userManager;

        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) 
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            
            var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);
            if (teacher == null) throw new UnauthorizedAccessException("Tylko nauczyciel może wystawiać oceny.");

            var grade = new Grade
            {
                StudentId = addGradeToStudentVm.StudentId,
                SubjectId = addGradeToStudentVm.SubjectId,
                GradeValue = addGradeToStudentVm.GradeValue,
                DateOfIssue = DateTime.Now
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            
            var grades = DbContext.Grades.Where(g => g.StudentId == getGradesVm.StudentId).ToList();

            return new GradesReportVm 
            { 
                Grades = Mapper.Map<System.Collections.Generic.IList<GradeVm>>(grades) 
            };
        }
    }
}