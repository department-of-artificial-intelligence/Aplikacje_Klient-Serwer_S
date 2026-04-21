using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.DAL.EF;
namespace SchoolRegister.Tests.UnitTests
{
    public abstract class BaseUnitTests
    {
        protected readonly ApplicationDbContext Dbcontext =null!;
        public BaseUnitTests (ApplicationDbContext dbContext)
        {
            Dbcontext=dbContext;
        }
    }
}