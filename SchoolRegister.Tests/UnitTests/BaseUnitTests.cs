using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.Services.Configuration.AutoMapperProfiles;
using SchoolRegister.Services.Interfaces;
namespace SchoolRegister.Tests.UnitTests;

public abstract class BaseUnitTests
{
    protected readonly ApplicationDbContext DbContext = null!;
    public BaseUnitTests(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }
}