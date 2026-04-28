using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SchoolRegister.Web.Controllers;

public class BaseController : Controller
{
    protected readonly ILogger Logger;
    protected readonly IMapper Mapper;
    protected readonly IStringLocalizer Localizer;

    public BaseController(ILogger logger, IMapper mapper, IStringLocalizer localizer)
    {
        Logger = logger;
        Mapper = mapper;
        Localizer = localizer;
    }
}
