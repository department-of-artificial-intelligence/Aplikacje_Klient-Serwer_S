using System.Linq;
using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using Xunit;

namespace SchoolRegister.Tests.UnitTests
{
    public class TeacherServiceUnitTests : BaseUnitTests
    {
        private readonly ITeacherService _teacher_service;

        public TeacherServiceUnitTests(ApplicationDbContext db_context, ITeacherService teacher_service) : base(db_context)
        {
            _teacher_service = teacher_service;
        }

        [Fact]
        public void GetTeacher()
        {
            var teacher = _teacher_service.GetTeacher(x => x.UserName == "t1@eg.eg");
            Assert.NotNull(teacher);
        }

        [Fact]
        public void GetTeachers()
        {
            var teachers = _teacher_service.GetTeachers(x => x.UserName.Contains("@eg.eg")).ToList();
            Assert.NotNull(teachers);
            Assert.NotEmpty(teachers);
            Assert.Equal(3, teachers.Count());
        }

        [Fact]
        public void GetAllTeachers()
        {
            var teachers = _teacher_service.GetTeachers().ToList();
            Assert.NotNull(teachers);
            Assert.NotEmpty(teachers);
            Assert.Equal(3, teachers.Count());
        }

        [Fact]
        public void GetTeachersGroups()
        {
            var get_teachers_group = new TeachersGroupsVm()
            {
                TeacherId = 1
            };

            var teachers_groups = _teacher_service.GetTeachersGroups(get_teachers_group).ToList();

            Assert.NotNull(teachers_groups);
            Assert.NotEmpty(teachers_groups);
            Assert.Equal(3, teachers_groups.Count());
        }
    }
}