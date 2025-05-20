using SchoolRegister.DAL.EF;

namespace SchoolRegister.Tests.UnitTests;
    public abstract class BaseUnitTests{
        protected readonly ApplicationDbContext dbContext;

        public BaseUnitTests (ApplicationDbContext context) {
            this.dbContext = context;
        }
    }