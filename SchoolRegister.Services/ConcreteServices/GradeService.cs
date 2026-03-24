using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.Services;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

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
        var student = DbContext.Users.OfType<Student>()
            .FirstOrDefault(s => s.Id == getGradesVm.StudentId);
        if (student == null)
            return new GradesReportVm();

        return new GradesReportVm
        {
            StudentName = $"{student.FirstName} {student.LastName}",
            Grades = Mapper.Map<IEnumerable<GradeVm>>(student.Grades)
        };
    }
}
