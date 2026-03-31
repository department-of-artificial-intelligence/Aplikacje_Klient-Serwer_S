using System;

namespace SchoolRegister.Tests.UnitTests;

public class GradeService : BaseService, IGradeService
{
    private readonly UserManager<User> _userManager;

    public GradeService(
        ApplicationDbContext dbContext, 
        IMapper mapper, 
        ILogger logger, 
        UserManager<User> userManager) 
        // Jeśli klasa BaseService przyjmuje parametry w konstruktorze, 
        // przekaż je używając słowa kluczowego 'base':
        // : base(dbContext, mapper, logger)
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
