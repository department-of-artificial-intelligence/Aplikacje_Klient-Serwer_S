using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Threading.Tasks;

namespace SchoolRegister.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class GradeApiController : BaseApiController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;

    public GradeApiController(ILogger<GradeApiController> logger, IMapper mapper,
        IGradeService gradeService,
        UserManager<User> userManager) : base(logger, mapper)
    {
        _gradeService = gradeService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public IActionResult Post([FromBody] AddGradeToStudentVm addGradeVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _gradeService.AddGradeToStudent(addGradeVm);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpGet("report/{studentId:int:min(1)}")]
    [Authorize(Roles = "Teacher, Parent, Student")]
    public async Task<IActionResult> GetReport(int studentId)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null)
                return Unauthorized();

            var vm = new GetGradeReportVm
            {
                StudentId = studentId,
                GetterUserId = user.Id
            };

            var report = _gradeService.GetGradesReportForStudent(vm);
            return Ok(report);
        }
        catch (UnauthorizedAccessException ex)
        {
            Logger.LogError(ex, ex.Message);
            return Forbid();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }
}