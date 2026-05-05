using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Admin")] 
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    private readonly IStudentService _studentService;

    public GroupController(IGroupService groupService, IStudentService studentService,
                           ILogger<GroupController> logger, IMapper mapper, 
                           IStringLocalizer<BaseController> localizer) 
        : base(logger, mapper, localizer)
    {
        _groupService = groupService;
        _studentService = studentService;
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
        return View();
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
    [HttpGet]
public IActionResult AttachStudentToGroup(int groupId)
{
    var students = _studentService.GetStudents(s => s.GroupId == null); // Tylko studenci bez grupy
    ViewBag.StudentsSelectList = new SelectList(students.Select(s => new {
        Value = s.Id,
        Text = $"{s.FirstName} {s.LastName}"
    }), "Value", "Text");
    
    return View(new AttachDetachStudentToGroupVm { GroupId = groupId });
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AttachStudentToGroup(AttachDetachStudentToGroupVm model)
{
    if (ModelState.IsValid)
    {
        _groupService.AttachStudentToGroup(model);
        return RedirectToAction("Index");
    }
    return View(model);
}
}