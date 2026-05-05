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
            var grade = _mapper.Map<Grade>(addGradeToStudentVm);
            grade.DateOfIssue = DateTime.Now;

            // Dodajemy do bazy
            _dbContext.Grades.Add(grade);
            _dbContext.SaveChanges();

            // Zwracamy zmapowany wynik
            return _mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            throw new NotImplementedException();
        }
    }
}