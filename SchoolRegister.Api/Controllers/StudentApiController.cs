using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using System;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
[Route("api/[controller]")]
public class StudentApiController : BaseApiController
{
    private readonly IStudentService _studentService;

    public StudentApiController(ILogger<StudentApiController> logger, IMapper mapper,
        IStudentService studentService) : base(logger, mapper)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var students = _studentService.GetStudents();
            return Ok(students);
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
            var student = _studentService.GetStudent(s => s.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }
}