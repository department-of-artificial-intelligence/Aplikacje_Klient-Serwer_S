using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
public class GroupApiController : BaseApiController
{
    private readonly IGroupService _groupService;

    public GroupApiController(ILogger<GroupApiController> logger, IMapper mapper, IGroupService groupService) : base(logger, mapper)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_groupService.GetGroups());
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        var group = _groupService.GetGroup(g => g.Id == id);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromBody] AddOrUpdateGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(_groupService.AddOrUpdateGroup(vm));
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult Put([FromBody] AddOrUpdateGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(_groupService.AddOrUpdateGroup(vm));
    }
}