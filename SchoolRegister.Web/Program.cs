using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Configuration.AutoMapperProfiles;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAutoMapper(typeof(MainProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<UserManager<User>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient (typeof (ILogger), typeof (Logger<Program>));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStringLocalizer, StringLocalizer<BaseController>> ();
builder.Services.AddScoped<ISubjectService, SubjectService> ();
builder.Services.AddScoped<IGradeService, GradeService> ();
builder.Services.AddScoped<IGroupService, GroupService> ();
builder.Services.AddScoped<IStudentService, StudentService> ();
builder.Services.AddScoped<ITeacherService, TeacherService> ();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();