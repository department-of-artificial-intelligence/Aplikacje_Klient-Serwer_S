using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Teacher, Parent, Student")]
public class GradeApiController : BaseApiController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;
    public GradeApiController(ILogger logger, IMapper mapper,
    IGradeService gradeService,
    UserManager<User> userManager) : base(logger, mapper)
    {
        _gradeService = gradeService;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                if (user is Student student)
                    return Ok(_gradeService.GetGradesReportForStudent(
                        new GetGradesReportVm {
                            StudentId = student.Id, 
                            GetterUserId = student.Id
                            }
                        )
                    );
                return BadRequest("Teacher is assigned to role, but to the Teacher type.");
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
    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            bool isParent = await _userManager.IsInRoleAsync(user, "Parent");
            bool isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
            
            if (isParent || isTeacher)
            {
                var report = _gradeService.GetGradesReportForStudent(
                    new GetGradesReportVm {
                        StudentId = id,
                        GetterUserId = id
                    }
                );
                if (report == null)
                    return NotFound();
                return Ok(report);
            }
            else
                return BadRequest("Error occurred");
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
    public IActionResult Post([FromBody] AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var subjectVm = _gradeService.AddGradeToStudent(addGradeToStudenVm);
            return Ok(subjectVm);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }


}