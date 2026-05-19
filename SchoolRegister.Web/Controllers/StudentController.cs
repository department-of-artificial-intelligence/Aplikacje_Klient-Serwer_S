using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Parent")]
public class StudentController : BaseController
{
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;
    public StudentController(
    IStudentService studentService,
    UserManager<User> userManager,
    IStringLocalizer localizer,
    ILogger logger,
    IMapper mapper) : base(logger, mapper, localizer)
    {
        _studentService = studentService;
        _userManager = userManager;
    }
     public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;
        if (_userManager.IsInRoleAsync(user, "Admin").Result)
            return View(_studentService.GetStudents());
        else if (_userManager.IsInRoleAsync(user, "Teacher").Result && user is Teacher teacher)
        {
            return View(_studentService
                .GetStudents(s => s.Group.SubjectGroups
                    .Any(sg => sg.Subject.TeacherId == teacher.Id)
                )
            );
        }
        else if (_userManager.IsInRoleAsync(user, "Parent").Result  && user is Parent parent)
            return View(_studentService.GetStudents(s => s.ParentId == parent.Id));
        else
            return View("Error");
    }
    public IActionResult Details(int id)
    {
        var studentVm = _studentService.GetStudent(s => s.Id == id);
        return View(studentVm);
    }
}