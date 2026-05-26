using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Teacher, Admin, Parent")]
public class StudentApiController : BaseApiController
{
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;
    public StudentApiController(ILogger logger, IMapper mapper,
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
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok(_studentService.GetStudents());

            else if (await _userManager.IsInRoleAsync(user, "Teacher") && user is Teacher teacher)
            {
                return Ok(_studentService
                    .GetStudents(s => s.Group.SubjectGroups
                        .Any(sg => sg.Subject.TeacherId == teacher.Id)
                    )
                );
            }
            else if (await _userManager.IsInRoleAsync(user, "Parent") && user is Parent parent)
            {
                return Ok(_studentService
                    .GetStudents(s => s.ParentId == parent.Id)
                );
            }
            else
                return BadRequest("Error occurred");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

     [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var studentVm = _studentService.GetStudent(s => s.Id == id);
        return Ok(studentVm);
    }
}