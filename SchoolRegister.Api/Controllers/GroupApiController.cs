using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
public class GroupApiController : BaseApiController
{
    private readonly IGroupService _groupService;

    public GroupApiController(ILogger<GroupApiController> logger, IMapper mapper,
                              IGroupService groupService) : base(logger, mapper)
    {
        _groupService = groupService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultGroup = _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
        return Ok(resultGroup);
    }

    [HttpPost("add_student")]
    [Authorize(Roles = "Admin, Teacher")]
    public IActionResult AddStudent(int studentId, int groupId)
    {
        try
        {
            _groupService.AddStudentToGroup(studentId, groupId);

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Wystąpił błąd przy dodawaniu studenta");
        }
    }

    [HttpDelete("remove_student")]
    [Authorize(Roles = "Admin, Teacher")]
    public IActionResult RemoveStudent(int studentId, int groupId)
    {
        try
        {
            _groupService.RemoveStudentFromGroup(studentId, groupId);

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Wystąpił błąd przy usuwaniu studenta");
        }
    }
}