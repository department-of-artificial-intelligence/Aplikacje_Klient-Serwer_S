using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Api.Controllers;


[Authorize(Roles = "Admin,Teacher,Parent,Student")]
public class StudentApiController : BaseApiController
{
    private readonly IStudentService _studentservice;
}