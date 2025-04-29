using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using Microsoft.AspNetCore.Identity;
namespace SchoolRegister.Services.ConcreteServices
{
 public class TeacherService :  BaseService,ITeacherService
 {
    private UserManager<User> _UserManager;
 }
}