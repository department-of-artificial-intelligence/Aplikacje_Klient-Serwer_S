using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Admin, Teacher, Student, Parent")]
public class StudentController : BaseController
{
    private readonly IStudentService _studentService;
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;

    public StudentController(IStudentService studentService,
        IGradeService gradeService,
        UserManager<User> userManager,
        IStringLocalizer localizer,
        ILogger logger,
        IMapper mapper) : base(logger, mapper, localizer)
    {
        _studentService = studentService;
        _gradeService = gradeService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;
        if (_userManager.IsInRoleAsync(user, "Admin").Result ||
            _userManager.IsInRoleAsync(user, "Teacher").Result)
            return View(_studentService.GetStudents());
        else if (_userManager.IsInRoleAsync(user, "Parent").Result && user is Parent parent)
            return View(_studentService.GetStudents(s => s.ParentId == parent.Id));
        else if (_userManager.IsInRoleAsync(user, "Student").Result)
            return RedirectToAction("Details", new { studentId = user.Id });
        return View("Error");
    }

    public IActionResult Details(int studentId)
    {
        var user = _userManager.GetUserAsync(User).Result;
        var getGradesReportVm = new GetGradesReportVm
        {
            StudentId = studentId,
            GetterUserId = user.Id
        };
        var report = _gradeService.GetGradesReportForStudent(getGradesReportVm);
        return View(report);
    }
}
