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

    private readonly ISubjectService _subjectService;
    public GroupController(IGroupService groupService, IStudentService studentService, ISubjectService subjectService, ILogger<GroupController> logger, IMapper mapper, IStringLocalizer<BaseController> localizer) : base(logger, mapper, localizer)
    {
        _groupService = groupService;
        _studentService = studentService;
        _subjectService = subjectService;
    }
    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();
        return View(groups);
    }
    [HttpGet]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var groupVm = _groupService.GetGroup(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateGroupVm>(groupVm));
        }
        ViewBag.ActionType = "Add";
        return View(new AddOrUpdateGroupVm());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            return RedirectToAction("Index");
        }
        return View(addOrUpdateGroupVm);
    }
    public IActionResult Details(int id)
    {
        var groupVm = _groupService.GetGroup(x => x.Id == id);
        var allStudents = _studentService.GetStudents();
        var studentsInGroupIds = groupVm.Students.Select(s => s.Id).ToList();
        var availableStudents = allStudents.Where(s => !studentsInGroupIds.Contains(s.Id)).ToList();
        ViewBag.StudentsSelectList = new SelectList(availableStudents.Select(s => new
        {
            Text = $"{s.FirstName} {s.LastName}",
            Value = s.Id
        }), "Value", "Text");
        return View(groupVm);
    }

[HttpGet]
public IActionResult AttachSubjectToGroup(int subjectId)
{
    var subjectVm = _subjectService.GetSubject(x => x.Id == subjectId);

    var allGroups = _groupService.GetGroups();

    var assignedGroupIds = subjectVm.Groups.Select(g => g.Id).ToList();

    var availableGroups = allGroups.Where(g => !assignedGroupIds.Contains(g.Id)).ToList();
    
    ViewBag.GroupsSelectList = new SelectList(availableGroups, "Id", "Name");

    var model = new AttachDetachSubjectGroupVm { SubjectId = subjectId };
    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AttachSubjectToGroup(AttachDetachSubjectGroupVm model)
{
    if (ModelState.IsValid)
    {
        _groupService.AttachSubjectToGroup(model);
        return RedirectToAction("Index", "Subject"); 
    }
    return View(model);
}

[HttpGet]
public IActionResult DetachSubjectToGroup(int subjectId) 
{
    // 1. Pobieramy konkretny przedmiot wraz z jego grupami
    // Zakładam, że Twój serwis ma metodę GetSubject, która zwraca model z listą grup (SubjectVm)
    var subjectVm = _subjectService.GetSubject(s => s.Id == subjectId);

    if (subjectVm == null || subjectVm.Groups == null)
    {
        return NotFound();
    }

    // 2. Tworzymy SelectList TYLKO z grup przypisanych do tego przedmiotu
    ViewBag.GroupsSelectList = new SelectList(subjectVm.Groups, "Id", "Name");

    // 3. Przekazujemy model do widoku
    var model = new AttachDetachSubjectGroupVm { SubjectId = subjectId };
    return View(model);
}
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult DetachSubjectToGroup(AttachDetachSubjectGroupVm model)
{
    if (ModelState.IsValid)
    {
        // Tutaj musisz wywołać odpowiednią metodę z serwisu do odłączania
        _groupService.DetachSubjectFromGroup(model); 
        return RedirectToAction("Index", "Subject");
    }
    return View(model);
}
}