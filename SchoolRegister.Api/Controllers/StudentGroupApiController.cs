using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin,Teacher,Parent,Student")]
public class StudentApiController : BaseApiController
{
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;

    public StudentApiController(
        ILogger logger,
        IMapper mapper,
        IStudentService studentService,
        UserManager<User> userManager) : base(logger, mapper)
    {
        _studentService = studentService;
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

            if (await _userManager.IsInRoleAsync(user, "Admin") ||
                await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                return Ok(_studentService.GetStudents());
            }

            if (await _userManager.IsInRoleAsync(user, "Parent"))
            {
                if (user is Parent parent)
                    return Ok(_studentService.GetStudents(s => s.ParentId == parent.Id));

                return BadRequest("Parent is assigned to role, but not to the Parent type.");
            }

            if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                if (user is Student student)
                    return Ok(_studentService.GetStudents(s => s.Id == student.Id));

                return BadRequest("Student is assigned to role, but not to the Student type.");
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

            if (await _userManager.IsInRoleAsync(user, "Admin") ||
                await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                var studentVm = _studentService.GetStudent(s => s.Id == id);
                if (studentVm == null)
                    return NotFound();

                return Ok(studentVm);
            }

            if (await _userManager.IsInRoleAsync(user, "Parent"))
            {
                if (user is Parent parent)
                {
                    var studentVm = _studentService.GetStudent(s => s.Id == id && s.ParentId == parent.Id);
                    if (studentVm == null)
                        return NotFound();

                    return Ok(studentVm);
                }

                return BadRequest("Parent is assigned to role, but not to the Parent type.");
            }

            if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                if (user is Student student)
                {
                    if (student.Id != id)
                        return Forbid();

                    var studentVm = _studentService.GetStudent(s => s.Id == id);
                    if (studentVm == null)
                        return NotFound();

                    return Ok(studentVm);
                }

                return BadRequest("Student is assigned to role, but not to the Student type.");
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
}