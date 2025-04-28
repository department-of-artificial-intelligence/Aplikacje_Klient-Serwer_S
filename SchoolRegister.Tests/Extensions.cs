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
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            // Roles
            var teacherRole = new Role { Id = 3, Name = "Teacher", RoleValue = RoleValue.Teacher };
            await roleManager.CreateAsync(teacherRole);

            var studentRole = new Role { Id = 1, Name = "Student", RoleValue = RoleValue.Student };
            await roleManager.CreateAsync(studentRole);

            var parentRole = new Role { Id = 2, Name = "Parent", RoleValue = RoleValue.Parent };
            await roleManager.CreateAsync(parentRole);

            var adminRole = new Role { Id = 4, Name = "Admin", RoleValue = RoleValue.Admin };
            await roleManager.CreateAsync(adminRole);

            // Groups
            await dbContext.Groups.AddRangeAsync(
                new Group { Id = 1, Name = "IO" },
                new Group { Id = 2, Name = "PAI" },
                new Group { Id = 3, Name = "AIP Erasmus" }
            );

            // User password
            var userPassword = "User1234";

            // Teachers
            var teachers = new[]
            {
                new Teacher { Id = 1, FirstName = "Adam", LastName = "Bednarski", UserName = "t1@eg.eg", Email = "real_email@eg.eg", Title = "mgr inż.", RegistrationDate = new DateTime(2010, 1, 1) },
                new Teacher { Id = 2, FirstName = "Jan", LastName = "Nowak", UserName = "t2@eg.eg", Email = "t2@eg.eg", Title = "mgr", RegistrationDate = new DateTime(2010, 11, 12) },
                new Teacher { Id = 12, FirstName = "Stanisław", LastName = "Nowakowski", UserName = "t11@eg.eg", Email = "t11@eg.eg", Title = "mgr inż.", RegistrationDate = new DateTime(2010, 11, 12) }
            };
            foreach (var teacher in teachers)
            {
                await userManager.CreateAsync(teacher, userPassword);
                await userManager.AddToRoleAsync(teacher, teacherRole.Name);
            }

            // Parents
            var parents = new[]
            {
                new Parent { Id = 3, FirstName = "Zbigniew", LastName = "Kowalski", UserName = "p1@eg.eg", Email = "real_email@eg.eg", RegistrationDate = new DateTime(2014, 3, 20) },
                new Parent { Id = 4, FirstName = "Anna", LastName = "Nowakowska", UserName = "p2@eg.eg", Email = "p2@eg.eg", RegistrationDate = new DateTime(2014, 6, 21) }
            };
            foreach (var parent in parents)
            {
                await userManager.CreateAsync(parent, userPassword);
                await userManager.AddToRoleAsync(parent, parentRole.Name);
            }

            // Students
            var students = new[]
            {
                new Student { Id = 5, FirstName = "Tomasz", LastName = "Kowalski", UserName = "s1@eg.eg", Email = "s1@eg.eg", RegistrationDate = new DateTime(2016, 5, 11), GroupId = 1, ParentId = 3 },
                new Student { Id = 6, FirstName = "Krzysztof", LastName = "Kowalski", UserName = "s2@eg.eg", Email = "s2@eg.eg", RegistrationDate = new DateTime(2015, 9, 18), GroupId = 1, ParentId = 3 },
                new Student { Id = 7, FirstName = "Natalia", LastName = "Kowalska", UserName = "s3@eg.eg", Email = "s3@eg.eg", RegistrationDate = new DateTime(2017, 7, 16), GroupId = 2, ParentId = 3 },
                new Student { Id = 8, FirstName = "Magdalena", LastName = "Wiśniewska", UserName = "s4@eg.eg", Email = "s4@eg.eg", RegistrationDate = new DateTime(2018, 5, 14), GroupId = 2, ParentId = 4 },
                new Student { Id = 9, FirstName = "Jan", LastName = "Wiśniewski", UserName = "s5@eg.eg", Email = "s5@eg.eg", RegistrationDate = new DateTime(2019, 2, 19), GroupId = 3, ParentId = 4 },
                new Student { Id = 10, FirstName = "Krystian", LastName = "Wiśniewski", UserName = "s6@eg.eg", Email = "s6@eg.eg", RegistrationDate = new DateTime(2019, 5, 1), GroupId = 3, ParentId = 4 }
            };
            foreach (var student in students)
            {
                await userManager.CreateAsync(student, userPassword);
                await userManager.AddToRoleAsync(student, studentRole.Name);
            }

            // Admin
            var admin = new User
            {
                Id = 11,
                FirstName = "Jacek",
                LastName = "Kowalczyk",
                UserName = "a1@eg.eg",
                Email = "a1@eg.eg",
                RegistrationDate = new DateTime(2009, 1, 1)
            };
            await userManager.CreateAsync(admin, userPassword);
            await userManager.AddToRoleAsync(admin, adminRole.Name);

            // Subjects
            await dbContext.Subjects.AddRangeAsync(
                new Subject { Id = 1, Name = "Aplikacje WWW", Description = "Aplikacje webowe", TeacherId = 1 },
                new Subject { Id = 2, Name = "Programowanie obiektowe", Description = "Programowanie obiektowe jest przedmiotem realizującym przykłady programowania obiektowego", TeacherId = 1 },
                new Subject { Id = 3, Name = "Advanced Internet Programming", Description = "Advanced Internet Programming is a course for ERASMUS+ students", TeacherId = 2 },
                new Subject { Id = 4, Name = "Administracja Internetowymi Systemami Baz Danych", Description = "Kontynuacja przedmiotu Bazy danych na studiach stacjonarnych I stopnia spec. PAI", TeacherId = 2 },
                new Subject { Id = 5, Name = "Programowanie interaktywnej grafiki dla stron WWW", TeacherId = 12 }
            );

            // SubjectGroups
            await dbContext.SubjectGroups.AddRangeAsync(
                new SubjectGroup { SubjectId = 1, GroupId = 1 },
                new SubjectGroup { SubjectId = 1, GroupId = 2 },
                new SubjectGroup { SubjectId = 2, GroupId = 1 },
                new SubjectGroup { SubjectId = 2, GroupId = 2 },
                new SubjectGroup { SubjectId = 2, GroupId = 3 },
                new SubjectGroup { SubjectId = 3, GroupId = 3 },
                new SubjectGroup { SubjectId = 4, GroupId = 2 },
                new SubjectGroup { SubjectId = 4, GroupId = 3 }
            );

            // Grades
            var grade1 = new Grade
            {
                DateOfIssue = new DateTime(2019, 3, 21, 17, 46, 38),
                StudentId = 5,
                SubjectId = 1,
                GradeValue = GradeScale.DB
            };
            await dbContext.Grades.AddAsync(grade1);

            // Save all changes
            await dbContext.SaveChangesAsync();
        }
    }
}
