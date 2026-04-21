using SchoolRegister.Services;
using SchoolRegister.DAL;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.Model.DataModels;



namespace SchoolRegister.Tests.UnitTests;

public class GradeServiceUnitTests : BaseUnitTests
{
    private readonly IGradeService _gradeService = null!;
    public GradeServiceUnitTests(ApplicationDbContext dbContext, IGradeService gradeService) : base(dbContext)
    {
        _gradeService = gradeService;
    }
    [Fact]
    public void AddGradeToStudent()
    {
        var gradeVm = new AddGradeToStudentVm()
        {
            StudentId = 5,
            SubjectId = 1,
            GradeValue = GradeScale.DB,
            TeacherId = 1
        };
        var grade = _gradeService.AddGradeToStudent(gradeVm);
        Assert.NotNull(grade);
        Assert.Equal(2, DbContext.Grades.Count());
    }
    [Fact]
    public void GetGradesReportForStudentByTeacher()
    {
        var getGradesReportForStudent = new GetGradesReportVm()
        {
            StudentId = 5,
            GetterUserId = 1
        };
        var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
        Assert.NotNull(gradesReport);
    }
    [Fact]
    public void GetGradesReportForStudentByStudent()
    {
        var getGradesReportForStudent = new GetGradesReportVm()
        {
            StudentId = 5,
            GetterUserId = 5
        };
        var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
        Assert.NotNull(gradesReport);
    }
    [Fact]
    public void GetGradesReportForStudentByParent()
    {
        var getGradesReportForStudent = new GetGradesReportVm()
        {
            StudentId = 5,
            GetterUserId = 3
        };
        var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
        Assert.NotNull(gradesReport);
    }

    private class AddGradeToStudentVm
    {
        public AddGradeToStudentVm()
        {
        }

        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public object GradeValue { get; set; }
        public int TeacherId { get; set; }
    }

    private class GetGradesReportVm
    {
        public GetGradesReportVm()
        {
        }

        public int StudentId { get; set; }
        public int GetterUserId { get; set; }
    }
}

internal interface IGradeService
{
    object? AddGradeToStudent(AddGradeToStudentVm gradeVm);
    object AddGradeToStudent(AddGradeToStudentVm gradeVm);
    object AddGradeToStudent(AddGradeToStudentVm gradeVm);
    object? GetGradesReportForStudent(GetGradesReportVm getGradesReportForStudent);
    object GetGradesReportForStudent(GetGradesReportVm getGradesReportForStudent);
}