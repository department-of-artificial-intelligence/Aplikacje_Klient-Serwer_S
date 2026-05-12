using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;

namespace SchoolRegister.Web.Controllers;
[Authorize(Roles = "Teacher, Admin, Student")]
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    public GroupController(IGroupService groupService,
        UserManager<User> userManager,
        IStringLocalizer localizer,
        ILogger<SubjectController> logger,
        IMapper mapper) : base(logger, mapper, localizer)
    {
        _groupService = groupService;
    }
}