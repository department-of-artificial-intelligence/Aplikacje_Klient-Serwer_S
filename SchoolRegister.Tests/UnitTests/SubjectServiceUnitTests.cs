using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using Xunit;

namespace SchoolRegister.Tests.UnitTests
{
    public class SubjectServiceUnitTests : BaseUnitTests
    {
        private readonly ISubjectService _subject_service = null!;

        public SubjectServiceUnitTests(ISubjectService subject_service, ApplicationDbContext db_context)
            : base(db_context)
        {
            _subject_service = subject_service;
        }

        [Fact]
        public void GetSubject()
        {
            var subject = _subject_service.GetSubject(x => x.Name == "Programowanie obiektowe");
            Assert.NotNull(subject);
        }

        [Fact]
        public void GetSubjects()
        {
            var subjects = _subject_service.GetSubjects(x => x.Id > 2 && x.Id <= 4).ToList();
            Assert.NotNull(subjects);
            Assert.NotEmpty(subjects);
            Assert.Equal(2, subjects.Count());
        }

        [Fact]
        public void GetAllSubjects()
        {
            var subjects = _subject_service.GetSubjects().ToList();
            Assert.NotNull(subjects);
            Assert.NotEmpty(subjects);
            Assert.Equal(DbContext.Subjects.Count(), subjects.Count());
        }

        [Fact]
        public void AddNewSubject()
        {
            var new_subject_vm = new AddOrUpdateSubjectVm()
            {
                Name = "Zaawansowane programowanie internetowe",
                Description = "W ramach przedmiotu studenci tworzą rozwiazania w bibliotekach SPA",
                TeacherId = 1
            };

            var created_subject = _subject_service.AddOrUpdateSubject(new_subject_vm);
            Assert.NotNull(created_subject);
            Assert.Equal("Zaawansowane programowanie internetowe", created_subject.Name);
        }

        [Fact]
        public void EditSubject()
        {
            var edit_subject_vm = new AddOrUpdateSubjectVm()
            {
                Id = 1,
                Name = "Aplikacje webowe",
                Description = null,
                TeacherId = 1
            };

            var edited_subject_vm = _subject_service.AddOrUpdateSubject(edit_subject_vm);
            Assert.NotNull(edited_subject_vm);
            Assert.Equal("Aplikacje webowe", edited_subject_vm.Name);
            Assert.Null(edited_subject_vm.Description);
        }
    }
}