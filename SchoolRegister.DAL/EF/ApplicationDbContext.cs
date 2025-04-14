using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

    // User discriminator (Student, Parent, Teacher)
    modelBuilder.Entity<User>()
        .HasDiscriminator<int>("UserType")
        .HasValue<User>((int)RoleValue.User)
        .HasValue<Student>((int)RoleValue.Student)
        .HasValue<Parent>((int)RoleValue.Parent)
        .HasValue<Teacher>((int)RoleValue.Teacher);

    // User -> Parent (self-reference)
    modelBuilder.Entity<Student>()
        .HasOne(s => s.Parent)
        .WithMany(p => p.Students)
        .HasForeignKey(s => s.ParentId)
        .OnDelete(DeleteBehavior.Restrict);

    // Student -> Group
    modelBuilder.Entity<Student>()
        .HasOne(s => s.Group)
        .WithMany(g => g.Students)
        .HasForeignKey(s => s.GroupId)
        .OnDelete(DeleteBehavior.Restrict);

    // Teacher -> Subject
    modelBuilder.Entity<Subject>()
        .HasOne(s => s.Teacher)
        .WithMany(t => t.Subjects)
        .HasForeignKey(s => s.TeacherId)
        .OnDelete(DeleteBehavior.Restrict);

    // Subject -> Grade
    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Subject)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.SubjectId)
        .OnDelete(DeleteBehavior.Restrict);

    // Student -> Grade
    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Student)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.StudentId)
        .OnDelete(DeleteBehavior.Restrict);
        
    modelBuilder.Entity<Grade>()
        .HasKey(g => new { g.SubjectId, g.StudentId });


    // SubjectGroup (many-to-many: Subject <-> Group)
    modelBuilder.Entity<SubjectGroup>(entity =>
    {
        entity.HasKey(sg => new { sg.SubjectId, sg.GroupId });

        entity.HasOne(sg => sg.Subject)
            .WithMany(s => s.SubjectGroups)
            .HasForeignKey(sg => sg.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(sg => sg.Group)
            .WithMany(g => g.SubjectGroups)
            .HasForeignKey(sg => sg.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    });
}

}
