using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Student, Parent, Admin")]
public class StudentController : BaseController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;
    private readonly IStudentService _studentService;


    public StudentController(IGradeService gradeService, 
                             UserManager<User> userManager, 
                             IStudentService studentService,
                             ILogger<StudentController> logger, 
                             IMapper mapper, 
                             IStringLocalizer<BaseController> localizer) 
        : base(logger, mapper, localizer)
    {
        _gradeService = gradeService;
        _userManager = userManager;
        _studentService = studentService;
    }

    public async Task<IActionResult> Index(int? studentId = null)
    {
    var user = await _userManager.GetUserAsync(User);
    int idToQuery = studentId ?? (user?.Id ?? 0); 

    var reportRequest = new GetGradesReportVm { StudentId = idToQuery };
    
    var gradesReport = _gradeService.GetGradesReportForStudent(reportRequest);
    
    return View(gradesReport); 
    }
    [Authorize(Roles = "Parent")]
public async Task<IActionResult> MyChildren()
{
    var user = await _userManager.GetUserAsync(User);
    var children = _studentService.GetStudents(s => s.ParentId == user.Id);
    return View(children);
}
}