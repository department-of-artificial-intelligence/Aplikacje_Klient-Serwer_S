using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin")]
public class GroupApiController : BaseApiController
{
    private readonly ISubjectService _subjectService;
    private readonly IStudentService _studentService;
    private readonly IGroupService _groupService;
    private readonly UserManager<User> _userManager;
    public GroupApiController(ISubjectService subjectService,
    IStudentService studentService,
    IGroupService groupService,
    UserManager<User> userManager,
    ILogger logger,
    IMapper mapper) : base(logger, mapper)
    {
        _subjectService = subjectService;
        _groupService = groupService;
        _studentService = studentService;

        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok(_groupService.GetGroups());
            else
                return BadRequest("Error");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var groupVm = _groupService.GetGroup(x => x.Id == id);
        return Ok(groupVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        return PostOrPutGroup( addOrUpdateGroupVm);
    }
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult Put([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
    // public IActionResult Put([FromBody] AddOrUpdateSubjectVm addOrUpdateSubjectVm, int id) // Optional Id param
    {
        return  PostOrPutGroup(addOrUpdateGroupVm);
    }
    [HttpDelete("{id:int:min(1)}")]
    [Authorize(Roles = "Admin")]


    [HttpPost("{groupId:int}/students/{studentId:int}")]
    [Authorize(Roles = "Admin")]

    public IActionResult AttachStudentToGroup(int groupId, int studentId)
    {
        return ExecuteServiceFunction(
            _groupService.AttachStudentToGroup,
            new AttachDetachStudentToGroupVm
            {
                GroupId = groupId,
                StudentId = studentId
            });
    }

    [HttpDelete("{groupId:int}/students/{studentId:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudentFromGroup(int groupId, int studentId)
    {
        return ExecuteServiceFunction(
            _groupService.DetachStudentFromGroup,
            new AttachDetachStudentToGroupVm
            {
                GroupId = groupId,
                StudentId = studentId
            });
    }



    // helper method
    private IActionResult PostOrPutGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var groupVm = _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            return Ok(groupVm);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    private IActionResult ExecuteServiceFunction<TVm, Res>(
        Func<TVm, Res> serviceMethod,
        TVm vm)
    {
        try
        {
            serviceMethod(vm);

            return NoContent();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }

}