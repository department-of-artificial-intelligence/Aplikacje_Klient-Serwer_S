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
[Authorize(Roles = "Teacher")]
public class GradeApiController : Controller
{
    private readonly IGradeService _gradeService;

    public GradeApiController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost]
    public IActionResult Add(AddGradeToStudentVm vm)
    {
        return Ok(_gradeService.AddGradeToStudent(vm));
    }

    [HttpGet("report")]
    public IActionResult Report(GetGradesReportVm vm)
    {
        return Ok(_gradeService.GetGradesReportForStudent(vm));
    }
}