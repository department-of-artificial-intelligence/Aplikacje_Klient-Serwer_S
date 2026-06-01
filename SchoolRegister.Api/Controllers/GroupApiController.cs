using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
[Route("api/[controller]")]
public class GroupApiController : BaseApiController
{
    private readonly IGroupService _groupService;

    public GroupApiController(ILogger<GroupApiController> logger, IMapper mapper, 
        IGroupService groupService) : base(logger, mapper)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_groupService.GetGroups());
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        try
        {
            var group = _groupService.GetGroup(g => g.Id == id);
            if (group == null)
                return NotFound();

            return Ok(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult PostOrPut([FromBody] AddOrUpdateGroupVm groupVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _groupService.AddOrUpdateGroup(groupVm);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("AttachStudent")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudent([FromBody] AttachDetachStudentToGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.AttachStudentToGroup(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }

    [HttpPost("DetachStudent")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudent([FromBody] AttachDetachStudentToGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.DetachStudentFromGroup(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }

    [HttpPost("AttachSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubject([FromBody] AttachDetachSubjectGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.AttachSubjectToGroup(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }

    [HttpPost("DetachSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubject([FromBody] AttachDetachSubjectGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.DetachSubjectFromGroup(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }

    [HttpPost("AttachTeacherToSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachTeacher([FromBody] AttachDetachSubjectToTeacherVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.AttachTeacherToSubject(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }

    [HttpPost("DetachTeacherFromSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachTeacher([FromBody] AttachDetachSubjectToTeacherVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(_groupService.DetachTeacherFromSubject(vm)); }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); return BadRequest("Error occurred"); }
    }
}