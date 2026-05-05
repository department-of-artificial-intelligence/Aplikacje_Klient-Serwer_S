using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

public class GradeController : Controller
{
    private readonly IGradeService _gradeService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;
    private readonly IGroupService _groupService;
    private readonly UserManager<User> _userManager;

    public GradeController(IGradeService gradeService, IStudentService studentService,
        ISubjectService subjectService, IGroupService groupService, UserManager<User> userManager)
    {
        _gradeService = gradeService;
        _studentService = studentService;
        _subjectService = subjectService;
        _groupService = groupService;
        _userManager = userManager;
    }

    // ─── ADD GRADE (Teacher only) ──────────────────────────────

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    public IActionResult AddGrade(int studentId)
    {
        var currentUser = _userManager.GetUserAsync(User).Result;

        // przedmioty tego nauczyciela
        var teacherSubjects = _subjectService.GetSubjects(s => s.TeacherId == currentUser!.Id).ToList();

        // grupy do których przypisane są przedmioty nauczyciela
        var groupIds = teacherSubjects
            .SelectMany(s => s.Groups ?? new List<GroupVm>())
            .Select(g => g.Id)
            .ToHashSet();

        // sprawdź czy student istnieje
        var student = _studentService.GetStudent(s => s.Id == studentId);
        if (student == null) return NotFound();

        // znajdź grupę studenta
        var studentGroup = _groupService.GetGroup(g =>
            g.Students != null && g.Students.Any(s => s.Id == studentId));

        // sprawdź czy student należy do grupy tego nauczyciela
        if (studentGroup == null || !groupIds.Contains(studentGroup.Id))
            return Forbid();

        // tylko przedmioty które są przypisane do grupy studenta
        var availableSubjects = teacherSubjects
            .Where(s => s.Groups != null && s.Groups.Any(g => g.Id == studentGroup.Id))
            .ToList();

        ViewBag.Student = student;
        ViewBag.TeacherId = currentUser!.Id;
        ViewBag.SubjectList = availableSubjects
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
        ViewBag.GradeList = Enum.GetValues<GradeScale>()
            .Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            }).ToList();

        return View(new AddGradeToStudentVm { StudentId = studentId, TeacherId = currentUser.Id });
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    [ValidateAntiForgeryToken]
    public IActionResult AddGrade(AddGradeToStudentVm vm)
    {
        var currentUser = _userManager.GetUserAsync(User).Result;

        // znajdź grupę studenta
        var studentGroup = _groupService.GetGroup(g =>
            g.Students != null && g.Students.Any(s => s.Id == vm.StudentId));

        // sprawdź czy teacher prowadzi ten przedmiot
        var subject = _subjectService.GetSubjects(s => s.Id == vm.SubjectId && s.TeacherId == currentUser!.Id)
            .FirstOrDefault();
        if (subject == null)
            ModelState.AddModelError("", "Nie możesz wystawić oceny z tego przedmiotu.");

        // sprawdź czy przedmiot jest przypisany do grupy studenta
        var subjectInGroup = subject?.Groups?.Any(g => g.Id == studentGroup?.Id) ?? false;
        if (!subjectInGroup)
            ModelState.AddModelError("", "Student nie uczęszcza na ten przedmiot.");

        if (!ModelState.IsValid)
        {
            var teacherSubjects = _subjectService.GetSubjects(s => s.TeacherId == currentUser!.Id).ToList();
            var availableSubjects = teacherSubjects
                .Where(s => s.Groups != null && s.Groups.Any(g => g.Id == studentGroup!.Id))
                .ToList();
            ViewBag.SubjectList = availableSubjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
            ViewBag.GradeList = Enum.GetValues<GradeScale>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                }).ToList();
            return View(vm);
        }

        _gradeService.AddGradeToStudent(vm);
        return RedirectToAction("Details", "Student", new { id = vm.StudentId });
    }

    // ─── GRADES REPORT (Student + Parent) ─────────────────────

    [HttpGet]
    [Authorize(Roles = "Student, Parent")]
    public IActionResult GradesReport()
    {
        var currentUser = _userManager.GetUserAsync(User).Result;

        if (User.IsInRole("Student"))
        {
            var report = _gradeService.GetGradesReportForStudent(new GetGradesReportVm
            {
                StudentId = currentUser!.Id,
                GetterUserId = currentUser.Id
            });
            return View(new List<GradesReportVm> { report });
        }

        if (User.IsInRole("Parent"))
        {
            var parent = _userManager.Users
                .OfType<Parent>()
                .Include(p => p.Students)
                .FirstOrDefault(p => p.Id == currentUser!.Id);

            if (parent?.Students == null || !parent.Students.Any())
                return View(new List<GradesReportVm>());

            var reports = parent.Students
                .Select(s => _gradeService.GetGradesReportForStudent(new GetGradesReportVm
                {
                    StudentId = s.Id,
                    GetterUserId = currentUser!.Id
                }))
                .ToList();

            return View(reports);
        }

        return Forbid();
    }
}