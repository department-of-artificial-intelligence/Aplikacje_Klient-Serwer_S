using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Student")]
public class StudentController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentController(
        IStudentService studentService,
        IStringLocalizer localizer,
        ILogger logger,
        IMapper mapper) : base(logger, mapper, localizer)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        return View(_studentService.GetStudents());
    }

    public IActionResult Details(int id)
    {
        var studentVm = _studentService.GetStudent(x => x.Id == id);
        if (studentVm == null)
            return View("Error");

        return View(studentVm);
    }
}