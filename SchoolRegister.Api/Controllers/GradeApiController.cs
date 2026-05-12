using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

public class GradeApiController : BaseApiController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;

    public GradeApiController(ILogger<GradeApiController> logger, IMapper mapper, IGradeService gradeService, UserManager<User> userManager) : base(logger, mapper)
    {
        _gradeService = gradeService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public IActionResult Post([FromBody] AddGradeToStudentVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = _gradeService.AddGradeToStudent(vm);
        return Ok(result);
    }

    [HttpGet("report/{studentId}")]
    [Authorize(Roles = "Student, Parent, Teacher, Admin")]
    public async Task<IActionResult> GetStudentGradesReport(int studentId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        var vm = new GetGradesReportVm { StudentId = studentId, GetterUserId = user.Id };
        
        var report = _gradeService.GetGradesReportForStudent(vm);
        if (report == null) return NotFound("Nie znaleziono raportu dla tego ucznia.");
        
        return Ok(report);
    }
}