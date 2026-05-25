using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class GroupApiController : Controller
{
    private readonly IGroupService _groupService;

    public GroupApiController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_groupService.GetGroups());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var group = _groupService.GetGroup(g => g.Id == id);

        if (group == null)
            return NotFound();

        return Ok(group);
    }

    [HttpPost]
    public IActionResult AddOrUpdate(AddOrUpdateGroupVm vm)
    {
        return Ok(_groupService.AddOrUpdateGroup(vm));
    }

    [HttpPost("attach-student")]
    public IActionResult AttachStudent(AttachDetachStudentToGroupVm vm)
    {
        return Ok(_groupService.AttachStudentToGroup(vm));
    }

    [HttpPost("detach-student")]
    public IActionResult DetachStudent(AttachDetachStudentToGroupVm vm)
    {
        return Ok(_groupService.DetachStudentFromGroup(vm));
    }

    [HttpPost("attach-subject")]
    public IActionResult AttachSubject(AttachDetachSubjectGroupVm vm)
    {
        return Ok(_groupService.AttachSubjectToGroup(vm));
    }

    [HttpPost("detach-subject")]
    public IActionResult DetachSubject(AttachDetachSubjectGroupVm vm)
    {
        return Ok(_groupService.DetachSubjectFromGroup(vm));
    }
}