using System;
using AutoMapper;
using Microsoft.Extensions.Logging; // Zmiana z Castle.Core.Logging
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

/// <summary>
/// Implementacja serwisu ocen
/// </summary>
public class GradeService : BaseService, IGradeService
{
    private readonly UserManager<User> _userManager;

    public GradeService(
        ApplicationDbContext dbContext, 
        IMapper mapper, 
        ILogger logger, 
        UserManager<User> userManager) 
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
    {
        // TODO: Dodaj logikę dodawania oceny dla ucznia
        throw new NotImplementedException();
    }

    public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
    {
        // TODO: Dodaj logikę pobierania raportu ocen ucznia
        throw new NotImplementedException();
    }
}