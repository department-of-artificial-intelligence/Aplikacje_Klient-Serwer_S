using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
[Route("api/[controller]")]
public class TeacherApiController : BaseApiController
{
    private readonly ITeacherService _teacherService;

    public TeacherApiController(ILogger<TeacherApiController> logger, IMapper mapper,
        ITeacherService teacherService) : base(logger, mapper)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var teachers = _teacherService.GetTeachers();
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        try
        {
            var teacher = _teacherService.GetTeacher(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    [HttpGet("{id:int:min(1)}/groups")]
    public IActionResult GetTeachersGroups(int id)
    {
        try
        {
            var vm = new TeachersGroupsVm { TeacherId = id };
            var groups = _teacherService.GetTeachersGroups(vm);
            return Ok(groups);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }
}