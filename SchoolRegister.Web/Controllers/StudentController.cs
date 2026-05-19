using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Student")]
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

    // Teacher i Admin widzą wszystkich, Student tylko siebie
    public IActionResult Index()
    {
        if (User.IsInRole("Student"))
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            return RedirectToAction("Details", new { id = currentUser!.Id });
        }
        return View(_studentService.GetStudents());
    }

    public IActionResult Details(int id)
    {
        // Student może widzieć tylko swoje dane
        if (User.IsInRole("Student"))
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser!.Id != id)
                return Forbid();
        }

        var studentVm = _studentService.GetStudent(x => x.Id == id);
        if (studentVm == null)
            return View("Error");

        return View(studentVm);
    }
}