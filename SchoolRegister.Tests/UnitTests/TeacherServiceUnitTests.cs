using SchoolRegister.Services.Interfaces;
using SchoolRegister.DAL.EF; 
using SchoolRegister.Tests.UnitTests;
using System.Linq.Expressions;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Tests.UnitTests;

public class TeacherServiceUnitTests : BaseUnitTests
{
    private readonly ITeacherService _teacherService = null!;
    
    public TeacherServiceUnitTests(ApplicationDbContext dbContext, ITeacherService teacherService) : base(dbContext)
    {
        _teacherService = teacherService;
    }
    
    [Fact]
    public void GetTeacher()
    {
        var teacher = _teacherService.GetTeacher(x => x.UserName == "t1@eg.eg");
        Assert.NotNull(teacher);
    }
    
    [Fact]
    public void GetTeachers()
    {
        var teachers = _teacherService.GetTeachers(x => x.UserName.Contains("@eg.eg"));
        Assert.NotNull(teachers);
        Assert.NotEmpty(teachers);
        Assert.Equal(3, teachers.Count());
    }
    
    [Fact]
    public void GetAllTeachers()
    {
        var teachers = _teacherService.GetTeachers();
        Assert.NotNull(teachers);
        Assert.NotEmpty(teachers);
        Assert.Equal(3, teachers.Count());
    }
    
    [Fact]
    public void GetTeachersGroups()
    {
        var getTeachersGroup = new TeachersGroupsVm
        {
            TeacherId = 1
        };
        var teachersGroups = _teacherService.GetTeachersGroups(getTeachersGroup);
        Assert.NotNull(teachersGroups);
        Assert.NotEmpty(teachersGroups);
        Assert.Equal(5, teachersGroups.Count());
    }
}

// --- NAPRAWIONE LOKALNE KLASY I INTERFEJSY ---

public class TeachersGroupsVm
{
    public int TeacherId { get; set; }
}

public interface ITeacherService
{
    object? GetTeacher(Expression<Func<User, bool>> expression);
    IEnumerable<object> GetTeachers(Expression<Func<User, bool>>? expression = null);
    IEnumerable<object> GetTeachersGroups(TeachersGroupsVm teachersGroupsVm);
}