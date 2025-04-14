using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // Włączamy lazy loading (pod warunkiem, że wszędzie w encjach
            // klucze nawigacyjne i kolekcje są 'virtual'):
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH - dziedziczenie User/Student/Parent/Teacher
            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<int>("UserType")
                .HasValue<User>((int)RoleValue.User)
                .HasValue<Student>((int)RoleValue.Student)
                .HasValue<Parent>((int)RoleValue.Parent)
                .HasValue<Teacher>((int)RoleValue.Teacher);

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

            modelBuilder.Entity<Subject>()
                .HasOne(t => t.Teacher)
                .WithMany(s => s.Subjects)
                .HasForeignKey(i => i.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(p => p.Parent)
                .WithMany(s => s.Students)
                .HasForeignKey(i => i.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.SubjectId, g.DateOfIssue });

            modelBuilder.Entity<Grade>()
                .HasOne(s => s.Student)
                .WithMany(g => g.Grades)
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        
            modelBuilder.Entity<Grade>()
                .HasOne(s => s.Subject)
                .WithMany(g => g.Grades)
                .HasForeignKey(i => i.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}