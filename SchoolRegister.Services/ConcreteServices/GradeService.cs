using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            if (addGradeToStudentVm == null) throw new ArgumentNullException(nameof(addGradeToStudentVm));

            // Mapujemy VM na model danych
            var grade = Mapper.Map<Grade>(addGradeToStudentVm);
            grade.DateOfIssue = DateTime.Now;

            // Dodajemy do bazy
            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            // Zwracamy zmapowany wynik
            return Mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            throw new NotImplementedException();
        }
    }
}