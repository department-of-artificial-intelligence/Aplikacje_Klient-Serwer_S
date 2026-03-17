
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    // table properties
    public virtual DbSet<Grade> Grades { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //configuration commands
        optionsBuilder.UseLazyLoadingProxies(); //enable lazy loading proxies
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Fluent API commands
        modelBuilder.Entity<User>()
        .ToTable("AspNetUsers")
        .HasDiscriminator<int>("UserType")
        .HasValue<User>((int)RoleValue.User)
        .HasValue<Student>((int)RoleValue.Student)
        .HasValue<Parent>((int)RoleValue.Parent)
        .HasValue<Teacher>((int)RoleValue.Teacher);

        modelBuilder.Entity<Student>()
        .Ignore(s => s.AverageGrade)
        .Ignore(s => s.AverageGradePerSubject)
        .Ignore(s => s.GradesPerSubject);

        modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId);
            
        modelBuilder.Entity<SubjectGroup>()
            .HasKey(sg => new { sg.GroupId, sg.SubjectId });
        modelBuilder.Entity<SubjectGroup>()
            .HasOne(g => g.Group)
            .WithMany(sg => sg.SubjectGroups)
            .HasForeignKey(g => g.GroupId);
        modelBuilder.Entity<SubjectGroup>()
            .HasOne(s => s.Subject)
            .WithMany(sg => sg.SubjectGroups)
            .HasForeignKey(s => s.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasKey( g=> new { g.SubjectId, g.StudentId, g.DateOfIssue });
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SubjectId);
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(s => s.TeacherId);
        
        modelBuilder.Entity<Parent>()
            .HasMany(p => p.Students)
            .WithOne(s => s.Parent)
            .HasForeignKey(s => s.ParentId);

    }
}
