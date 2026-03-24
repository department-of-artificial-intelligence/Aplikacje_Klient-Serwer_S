using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Tests
{
    public static class Extensions
    {
        // Create sample data
        public static async void SeedData(this IServiceCollection services)
        {
            var service_provider = services.BuildServiceProvider();
            var db_context = service_provider.GetRequiredService<ApplicationDbContext>();
            var user_manager = service_provider.GetRequiredService<UserManager<User>>();
            var role_manager = service_provider.GetRequiredService<RoleManager<Role>>();

            // Roles
            var teacher_role = new Role()
            {
                Id = 3,
                Name = "Teacher",
                RoleValue = RoleValue.Teacher
            };
            await role_manager.CreateAsync(teacher_role);

            var student_role = new Role()
            {
                Id = 1,
                Name = "Student",
                RoleValue = RoleValue.Student
            };
            await role_manager.CreateAsync(student_role);

            var parent_role = new Role()
            {
                Id = 2,
                Name = "Parent",
                RoleValue = RoleValue.Parent
            };
            await role_manager.CreateAsync(parent_role);

            var admin_role = new Role()
            {
                Id = 4,
                Name = "Admin",
                RoleValue = RoleValue.Admin
            };
            await role_manager.CreateAsync(admin_role);

            // Groups
            var group_io = new Group()
            {
                Id = 1,
                Name = "IO"
            };
            await db_context.Groups.AddAsync(group_io);

            var group_pai = new Group()
            {
                Id = 2,
                Name = "PAI"
            };
            await db_context.Groups.AddAsync(group_pai);

            var group_aip_erasmus = new Group()
            {
                Id = 3,
                Name = "AIP Erasmus"
            };
            await db_context.Groups.AddAsync(group_aip_erasmus);

            // All users, including teachers, students and parents
            var user_password = "User1234!"; // in lab env we use the same pass for all users :)

            var t_1 = new Teacher()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Bednarski",
                UserName = "t1@eg.eg",
                Email = "real_email@eg.eg",
                Title = "mgr inż.",
                RegistrationDate = new DateTime(2010, 1, 1)
            };
            await user_manager.CreateAsync(t_1, user_password);
            await user_manager.AddToRoleAsync(t_1, teacher_role.Name);

            var t_2 = new Teacher()
            {
                Id = 2,
                FirstName = "Jan",
                LastName = "Nowak",
                UserName = "t2@eg.eg",
                Email = "t2@eg.eg",
                RegistrationDate = new DateTime(2018, 11, 12),
                Title = "mgr"
            };
            await user_manager.CreateAsync(t_2, user_password);
            await user_manager.AddToRoleAsync(t_2, teacher_role.Name);

            var t_3 = new Teacher()
            {
                Id = 12,
                FirstName = "Stanisław",
                LastName = "Nowakowski",
                UserName = "t12@eg.eg",
                Email = "t12@eg.eg",
                Title = "mgr inż.",
                RegistrationDate = new DateTime(2010, 11, 12)
            };
            await user_manager.CreateAsync(t_3, user_password);
            await user_manager.AddToRoleAsync(t_3, teacher_role.Name);

            var p_1 = new Parent()
            {
                Id = 3,
                FirstName = "Zbigniew",
                LastName = "Kowalski",
                UserName = "p1@eg.eg",
                Email = "real_email@eg.eg",
                RegistrationDate = new DateTime(2014, 03, 20)
            };
            await user_manager.CreateAsync(p_1, user_password);
            await user_manager.AddToRoleAsync(p_1, parent_role.Name);

            var p_2 = new Parent()
            {
                Id = 4,
                FirstName = "Anna",
                LastName = "Nowakowska",
                UserName = "p2@eg.eg",
                Email = "p2@eg.eg",
                RegistrationDate = new DateTime(2014, 06, 21)
            };
            await user_manager.CreateAsync(p_2, user_password);
            await user_manager.AddToRoleAsync(p_2, parent_role.Name);

            var s_1 = new Student()
            {
                Id = 5,
                FirstName = "Tomasz",
                LastName = "Kowalski",
                UserName = "s1@eg.eg",
                Email = "s1@eg.eg",
                RegistrationDate = new DateTime(2016, 05, 11),
                GroupId = 1,
                ParentId = 3
            };
            await user_manager.CreateAsync(s_1, user_password);
            await user_manager.AddToRoleAsync(s_1, student_role.Name);

            var s_2 = new Student()
            {
                Id = 6,
                FirstName = "Krzysztof",
                LastName = "Kowalski",
                UserName = "s2@eg.eg",
                Email = "s2@eg.eg",
                RegistrationDate = new DateTime(2015, 09, 18),
                GroupId = 1,
                ParentId = 3
            };
            await user_manager.CreateAsync(s_2, user_password);
            await user_manager.AddToRoleAsync(s_2, student_role.Name);

            var s_3 = new Student()
            {
                Id = 7,
                FirstName = "Natalia",
                LastName = "Kowalska",
                UserName = "s3@eg.eg",
                Email = "s3@eg.eg",
                RegistrationDate = new DateTime(2017, 07, 16),
                GroupId = 2,
                ParentId = 3
            };
            await user_manager.CreateAsync(s_3, user_password);
            await user_manager.AddToRoleAsync(s_3, student_role.Name);

            var s_4 = new Student()
            {
                Id = 8,
                FirstName = "Magdalena",
                LastName = "Wiśniewska",
                UserName = "s4@eg.eg",
                Email = "s4@eg.eg",
                RegistrationDate = new DateTime(2018, 05, 14),
                GroupId = 2,
                ParentId = 4
            };
            await user_manager.CreateAsync(s_4, user_password);
            await user_manager.AddToRoleAsync(s_4, student_role.Name);

            var s_5 = new Student()
            {
                Id = 9,
                FirstName = "Jan",
                LastName = "Wiśniewski",
                UserName = "s5@eg.eg",
                Email = "s5@eg.eg",
                RegistrationDate = new DateTime(2019, 02, 19),
                GroupId = 3,
                ParentId = 4
            };
            await user_manager.CreateAsync(s_5, user_password);
            await user_manager.AddToRoleAsync(s_5, student_role.Name);

            var s_6 = new Student()
            {
                Id = 10,
                FirstName = "Krystian",
                LastName = "Wiśniewski",
                UserName = "s6@eg.eg",
                Email = "s6@eg.eg",
                RegistrationDate = new DateTime(2019, 05, 1),
                GroupId = 3,
                ParentId = 4
            };
            await user_manager.CreateAsync(s_6, user_password);
            await user_manager.AddToRoleAsync(s_6, student_role.Name);

            var a_1 = new User()
            {
                Id = 11,
                FirstName = "Jacek",
                LastName = "Kowalczyk",
                UserName = "a1@eg.eg",
                Email = "a1@eg.eg",
                RegistrationDate = new DateTime(2009, 1, 1)
            };
            await user_manager.CreateAsync(a_1, user_password);
            await user_manager.AddToRoleAsync(a_1, admin_role.Name);

            // Subject
            var subject_1 = new Subject()
            {
                Id = 1,
                Name = "Aplikacje WWW",
                Description = "Aplikacje webowe",
                TeacherId = 1
            };
            await db_context.AddAsync(subject_1);

            var subject_2 = new Subject()
            {
                Id = 2,
                Name = "Programowanie obiektowe",
                Description = "Programowanie obiektowe jest przedmiotem realizującym przykłady programowanie obiektowego",
                TeacherId = 1
            };
            await db_context.AddAsync(subject_2);

            var subject_3 = new Subject()
            {
                Id = 3,
                Name = "Advanced Internet Programming",
                Description = "Advanced Internet Programming is a course for ERASMUS+ students",
                TeacherId = 2
            };
            await db_context.AddAsync(subject_3);

            var subject_4 = new Subject()
            {
                Id = 4,
                Name = "Administracja Intenetowymi Systemami Baz Danych",
                Description = "Administracja Intenetowymi Systemami Baz Danych jest kontynuacją przedmiotu Bazy danych na studiach stacjonarnych I-go stopnia spec. PAI",
                TeacherId = 2
            };
            await db_context.AddAsync(subject_4);

            var subject_5 = new Subject()
            {
                Id = 5,
                Name = "Programowanie interaktywnej grafiki dla stron WWW",
                TeacherId = 12
            };
            await db_context.AddAsync(subject_5);

            // SubjectGroups
            var subject_group_1 = new SubjectGroup()
            {
                SubjectId = 1,
                GroupId = 1
            };
            await db_context.SubjectGroups.AddAsync(subject_group_1);

            var subject_group_2 = new SubjectGroup()
            {
                SubjectId = 1,
                GroupId = 2
            };
            await db_context.SubjectGroups.AddAsync(subject_group_2);

            var subject_group_3 = new SubjectGroup()
            {
                SubjectId = 2,
                GroupId = 1
            };
            await db_context.SubjectGroups.AddAsync(subject_group_3);

            var subject_group_4 = new SubjectGroup()
            {
                SubjectId = 2,
                GroupId = 2
            };
            await db_context.SubjectGroups.AddAsync(subject_group_4);

            var subject_group_5 = new SubjectGroup()
            {
                SubjectId = 2,
                GroupId = 3
            };
            await db_context.SubjectGroups.AddAsync(subject_group_5);

            var subject_group_6 = new SubjectGroup()
            {
                SubjectId = 3,
                GroupId = 3
            };
            await db_context.SubjectGroups.AddAsync(subject_group_6);

            var subject_group_7 = new SubjectGroup()
            {
                SubjectId = 4,
                GroupId = 2
            };
            await db_context.SubjectGroups.AddAsync(subject_group_7);

            var subject_group_8 = new SubjectGroup()
            {
                SubjectId = 4,
                GroupId = 3
            };
            await db_context.SubjectGroups.AddAsync(subject_group_8);

            var grade_1 = new Grade()
            {
                DateOfIssue = new DateTime(2019, 03, 21, 17, 46, 38),
                StudentId = 5,
                SubjectId = 1,
                GradeValue = GradeScale.DB
            };
            await db_context.Grades.AddAsync(grade_1);

            await db_context.SaveChangesAsync();
        }
    }
}