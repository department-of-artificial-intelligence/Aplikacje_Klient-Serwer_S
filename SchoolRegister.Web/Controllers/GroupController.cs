using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

public class GroupController : Controller
{
    private readonly IGroupService _groupService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;

    public GroupController(IGroupService groupService, IStudentService studentService, ISubjectService subjectService)
    {
        _groupService = groupService;
        _studentService = studentService;
        _subjectService = subjectService;
    }

    // ─── GROUPS ────────────────────────────────────────────────

    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();
        return View(groups);
    }

    public IActionResult Details(int id)
    {
        var group = _groupService.GetGroup(g => g.Id == id);
        if (group == null) return NotFound();

        var studentsInGroup = group.Students?.Select(s => s.Id).ToHashSet() ?? new HashSet<int>();
        ViewBag.AvailableStudents = _studentService.GetStudents()
            .Where(s => !studentsInGroup.Contains(s.Id))
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.FirstName} {s.LastName}"
            }).ToList();

        var subjectsInGroup = group.Subjects?.Select(s => s.Id).ToHashSet() ?? new HashSet<int>();
        ViewBag.AvailableSubjects = _subjectService.GetSubjects()
            .Where(s => !subjectsInGroup.Contains(s.Id))
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

        return View(group);
    }

    // ─── ADD / EDIT GROUP ──────────────────────────────────────

    [HttpGet]
    public IActionResult AddOrEditGroup(int? id)
    {
        if (id == null)
        {
            ViewBag.ActionType = "Add";
            return View(new AddOrUpdateGroupVm());
        }
        var group = _groupService.GetGroup(g => g.Id == id);
        if (group == null) return NotFound();
        ViewBag.ActionType = "Edit";
        return View(new AddOrUpdateGroupVm { Id = group.Id, Name = group.Name });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        _groupService.AddOrUpdateGroup(vm);
        return RedirectToAction("Index");
    }

    // ─── STUDENTS ──────────────────────────────────────────────

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddStudentToGroup(AttachDetachStudentToGroupVm vm)
    {
        _groupService.AttachStudentToGroup(vm);
        return RedirectToAction("Details", new { id = vm.GroupId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveStudentFromGroup(AttachDetachStudentToGroupVm vm)
    {
        _groupService.DetachStudentFromGroup(vm);
        return RedirectToAction("Details", new { id = vm.GroupId });
    }

    // ─── SUBJECTS ──────────────────────────────────────────────

    [HttpGet]
    public IActionResult AttachSubjectToGroup(int subjectId)
    {
        var groups = _groupService.GetGroups();
        ViewBag.GroupList = groups.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Name
        }).ToList();
        ViewBag.SubjectId = subjectId;
        ViewBag.ActionType = "Attach";
        return View("AttachDetachSubjectToGroup");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddSubjectToGroup(AttachDetachSubjectGroupVm vm)
    {
        _groupService.AttachSubjectToGroup(vm);
        return RedirectToAction("Index", "Subject");
    }

    [HttpGet]
    public IActionResult DetachSubjectToGroup(int subjectId)
    {
        var groups = _groupService.GetGroups(g =>
            g.SubjectGroups != null && g.SubjectGroups.Any(sg => sg.SubjectId == subjectId));
        ViewBag.GroupList = groups.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Name
        }).ToList();
        ViewBag.SubjectId = subjectId;
        ViewBag.ActionType = "Detach";
        return View("AttachDetachSubjectToGroup");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveSubjectFromGroup(AttachDetachSubjectGroupVm vm)
    {
        _groupService.DetachSubjectFromGroup(vm);
        return RedirectToAction("Index", "Subject");
    }
}