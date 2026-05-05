using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin")] // Nauczyciel wystawia oceny
public class GradeController : BaseController
{
    private readonly IGradeService _gradeService;
    private readonly ISubjectService _subjectService;
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;

    public GradeController(IGradeService gradeService, ISubjectService subjectService,
                           IStudentService studentService, UserManager<User> userManager,
                           ILogger<GradeController> logger, IMapper mapper,
                           IStringLocalizer<BaseController> localizer)
        : base(logger, mapper, localizer)
    {
        _gradeService = gradeService;
        _subjectService = subjectService;
        _studentService = studentService;
        _userManager = userManager;
    }

    // Widok formularza wystawiania oceny
    [HttpGet]
    public async Task<IActionResult> AddGrade()
    {
        var user = await _userManager.GetUserAsync(User);

        // Pobieramy przedmioty przypisane do danego nauczyciela
        var subjects = _subjectService.GetSubjects(s => s.TeacherId == user.Id);
        ViewBag.SubjectsSelectList = new SelectList(subjects, "Id", "Name");

        // Pobieramy wszystkich studentów
        var students = _studentService.GetStudents();
        ViewBag.StudentsSelectList = new SelectList(students.Select(s => new
        {
            Value = s.Id,
            Text = $"{s.FirstName} {s.LastName}"
        }), "Value", "Text");

        // Lista wartości ocen
        var grades = Enum.GetValues(typeof(GradeScale)).Cast<GradeScale>();
        ViewBag.GradesSelectList = new SelectList(grades.Select(g => new
        {
            Value = (int)g,
            Text = g.ToString()
        }), "Value", "Text");

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGrade(AddGradeToStudentVm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            model.TeacherId = user.Id;

            _gradeService.AddGradeToStudent(model);
            return RedirectToAction("Index", "Subject");
        }

        var currentUser = await _userManager.GetUserAsync(User);

        var subjects = _subjectService.GetSubjects(s => s.TeacherId == currentUser.Id);
        ViewBag.SubjectsSelectList = new SelectList(subjects, "Id", "Name");

        var students = _studentService.GetStudents();
        ViewBag.StudentsSelectList = new SelectList(students.Select(s => new
        {
            Value = s.Id,
            Text = $"{s.FirstName} {s.LastName}"
        }), "Value", "Text");

        var grades = Enum.GetValues(typeof(GradeScale)).Cast<GradeScale>();
        ViewBag.GradesSelectList = new SelectList(grades.Select(g => new
        {
            Value = (int)g,
            Text = g.ToString()
        }), "Value", "Text");

        return View(model);
    }
}