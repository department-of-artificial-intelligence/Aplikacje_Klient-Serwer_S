using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Admin,Teacher")]
public class GroupController : Controller
{
    private readonly IGroupService _groupService;
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public GroupController(IGroupService groupService, IMapper mapper, IStudentService studentService)
    {
        _groupService = groupService;
        _mapper = mapper;
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();

        ViewBag.Students = _studentService.GetStudents();

        return View(groups);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var group = _groupService.GetGroup(g => g.Id == id.Value);
            if (group == null) return NotFound();
            return View(_mapper.Map<AddOrUpdateGroupVm>(group));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm groupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddOrUpdateGroup(groupVm);
            return RedirectToAction("Index");
        }

        return View(groupVm);
    }

    public IActionResult AttachSubjectToGroup(int subjectId, int groupId)
    {
        var vm = new AttachDetachSubjectGroupVm
        {
            SubjectId = subjectId,
            GroupId = groupId
        };

        _groupService.AttachSubjectToGroup(vm);
        return RedirectToAction("Index", "Subject");
    }

    public IActionResult DetachSubjectToGroup(int subjectId, int groupId)
    {
        var vm = new AttachDetachSubjectGroupVm
        {
            SubjectId = subjectId,
            GroupId = groupId
        };

        _groupService.DetachSubjectFromGroup(vm);
        return RedirectToAction("Index", "Subject");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudentToGroup(int studentId, int groupId)
    {
        var vm = new AttachDetachStudentToGroupVm
        {
            StudentId = studentId,
            GroupId = groupId
        };

        _groupService.AttachStudentToGroup(vm);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudentFromGroup(int studentId, int groupId)
    {
        var vm = new AttachDetachStudentToGroupVm
        {
            StudentId = studentId,
            GroupId = groupId
        };

        _groupService.DetachStudentFromGroup(vm);
        return RedirectToAction("Index");
    }
}