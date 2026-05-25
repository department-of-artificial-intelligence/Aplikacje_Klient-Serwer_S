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
[Authorize(Roles = "Admin")]
public class TeacherApiController : Controller
{
    private readonly ITeacherService _teacherService;

    public TeacherApiController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_teacherService.GetTeachers());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var teacher = _teacherService.GetTeacher(t => t.Id == id);
        if (teacher == null)
            return NotFound();

        return Ok(teacher);
    }

    [HttpPost("groups")]
    public IActionResult GetGroups(TeachersGroupsVm vm)
    {
        return Ok(_teacherService.GetTeachersGroups(vm));
    }
}