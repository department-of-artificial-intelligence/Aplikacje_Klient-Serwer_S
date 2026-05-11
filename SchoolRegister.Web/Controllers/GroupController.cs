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

[Authorize(Roles = "Admin, Teacher")]
public class GroupController : BaseController
{
  private readonly IGroupService _groupService;
  private readonly IStudentService _studentService;
  private readonly UserManager<User> _userManager;

  public GroupController(IGroupService groupService,
      IStudentService studentService,
      UserManager<User> userManager,
      IStringLocalizer localizer,
      ILogger logger,
      IMapper mapper) : base(logger, mapper, localizer)
  {
    _groupService = groupService;
    _studentService = studentService;
    _userManager = userManager;
  }

  public IActionResult Index()
  {
    return View(_groupService.GetGroups());
  }

  [HttpGet]
  [Authorize(Roles = "Admin")]
  public IActionResult AddOrEditGroup(int? id = null)
  {
    if (id.HasValue)
    {
      var groupVm = _groupService.GetGroup(x => x.Id == id.Value);

      if (groupVm == null)
        return NotFound();

      ViewBag.ActionType = "Edit";

      var mapped = Mapper.Map<AddOrUpdateGroupVm>(groupVm);

      return View(mapped);
    }
    ViewBag.ActionType = "Add";
    return View();
  }

  [HttpGet]
  [Authorize(Roles = "Admin")]
  public IActionResult AttachSubjectToGroup(int subjectId)
  {
    var groups = _groupService.GetGroups();
    ViewBag.GroupsSelectList = new SelectList(groups.Select(g => new
    {
      Text = g.Name,
      Value = g.Id
    }), "Value", "Text");
    ViewBag.SubjectId = subjectId;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = "Admin")]
  public IActionResult AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
  {
    if (ModelState.IsValid)
    {
      _groupService.AttachSubjectToGroup(vm);
      return RedirectToAction("Index", "Subject");
    }
    return View();
  }

  [HttpGet]
  [Authorize(Roles = "Admin")]
  public IActionResult DetachSubjectToGroup(int subjectId)
  {
    var groups = _groupService.GetGroups();
    ViewBag.GroupsSelectList = new SelectList(groups.Select(g => new
    {
      Text = g.Name,
      Value = g.Id
    }), "Value", "Text");
    ViewBag.SubjectId = subjectId;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = "Admin")]
  public IActionResult DetachSubjectToGroup(AttachDetachSubjectGroupVm vm)
  {
    if (ModelState.IsValid)
    {
      _groupService.DetachSubjectFromGroup(vm);
      return RedirectToAction("Index", "Subject");
    }
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = "Admin")]
  public IActionResult AddOrEditGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
  {
    if (ModelState.IsValid)
    {
      _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
      return RedirectToAction("Index");
    }
    return View();
  }

  [HttpGet]
  [Authorize(Roles = "Admin")]
  public IActionResult AttachStudentToGroup()
  {
    var students = _studentService.GetStudents();
    var groups = _groupService.GetGroups();
    ViewBag.StudentsSelectList = new SelectList(students.Select(s => new
    {
      Text = $"{s.FirstName} {s.LastName}",
      Value = s.Id
    }), "Value", "Text");
    ViewBag.GroupsSelectList = new SelectList(groups.Select(g => new
    {
      Text = g.Name,
      Value = g.Id
    }), "Value", "Text");
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = "Admin")]
  public IActionResult AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
  {
    if (ModelState.IsValid)
    {
      _groupService.AttachStudentToGroup(vm);
      return RedirectToAction("Index");
    }
    return View();
  }

  [HttpGet]
  [Authorize(Roles = "Admin")]
  public IActionResult DetachStudentFromGroup()
  {
    var students = _studentService.GetStudents();
    var groups = _groupService.GetGroups();
    ViewBag.StudentsSelectList = new SelectList(students.Select(s => new
    {
      Text = $"{s.FirstName} {s.LastName}",
      Value = s.Id
    }), "Value", "Text");
    ViewBag.GroupsSelectList = new SelectList(groups.Select(g => new
    {
      Text = g.Name,
      Value = g.Id
    }), "Value", "Text");
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = "Admin")]
  public IActionResult DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
  {
    if (ModelState.IsValid)
    {
      _groupService.DetachStudentFromGroup(vm);
      return RedirectToAction("Index");
    }
    return View();
  }
}