using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin,Teacher")]
public class GroupApiController : BaseApiController
{
    private readonly IGroupService _groupService;
    private readonly UserManager<User> _userManager;

    public GroupApiController(
        ILogger logger,
        IMapper mapper,
        IGroupService groupService,
        UserManager<User> userManager) : base(logger, mapper)
    {
        _groupService = groupService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (user == null)
                return Unauthorized();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok(_groupService.GetGroups());

            if (await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                if (user is Teacher teacher)
                {
                    var groups = _groupService.GetGroups(g =>
                        g.SubjectGroups.Any(sg => sg.Subject.TeacherId == teacher.Id));
                    return Ok(groups);
                }

                return BadRequest("Teacher is assigned to role, but not to the Teacher type.");
            }

            return Forbid();
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
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (user == null)
                return Unauthorized();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var group = _groupService.GetGroup(g => g.Id == id);
                if (group == null)
                    return NotFound();

                return Ok(group);
            }

            if (await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                if (user is Teacher teacher)
                {
                    var group = _groupService.GetGroup(g =>
                        g.Id == id &&
                        g.SubjectGroups.Any(sg => sg.Subject.TeacherId == teacher.Id));

                    if (group == null)
                        return NotFound();

                    return Ok(group);
                }

                return BadRequest("Teacher is assigned to role, but not to the Teacher type.");
            }

            return Forbid();
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        return PostOrPutGroup(addOrUpdateGroupVm);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult Put([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        return PostOrPutGroup(addOrUpdateGroupVm);
    }

    [HttpPost("AttachStudent")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudent([FromBody] AttachDetachStudentToGroupVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _groupService.AttachStudentToGroup(vm);
            return Ok(student);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("DetachStudent")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudent([FromBody] AttachDetachStudentToGroupVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _groupService.DetachStudentFromGroup(vm);
            return Ok(student);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("AttachSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubject([FromBody] AttachDetachSubjectGroupVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _groupService.AttachSubjectToGroup(vm);
            return Ok(group);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("DetachSubject")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubject([FromBody] AttachDetachSubjectGroupVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _groupService.DetachSubjectFromGroup(vm);
            return Ok(group);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("AttachTeacher")]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachTeacher([FromBody] AttachDetachSubjectToTeacherVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subject = _groupService.AttachTeacherToSubject(vm);
            return Ok(subject);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpPost("DetachTeacher")]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachTeacher([FromBody] AttachDetachSubjectToTeacherVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subject = _groupService.DetachTeacherFromSubject(vm);
            return Ok(subject);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

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
}