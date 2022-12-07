using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DAL;
using School.MVC.DAL.Database;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), b =>
        b.MigrationsAssembly("School.MVC")));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

builder.Services.AddScoped<IRepositoriesService, RepositoriesService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IClassTypeService, ClassTypeService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/");

app.Map("/FillTables", (IApplicationBuilder appBuilder) =>
{
    appBuilder.Run(async context =>
    {
        IRepositoriesService repositoryManager = context.RequestServices.GetService<IRepositoriesService>();
        IEnumerable<Class> classes = await repositoryManager.ClassRepository.Get(20, "Class20");

        if (classes.Count() == 0)
        {
            DbDataConfigurator.FillModelArrays();

            repositoryManager.ClassRepository.Create(DbDataConfigurator.Classes);
            repositoryManager.ClassTypeRepository.Create(DbDataConfigurator.ClassTypes);
            repositoryManager.ScheduleRepository.Create(DbDataConfigurator.Schedules);
            repositoryManager.StudentRepository.Create(DbDataConfigurator.Students);
            repositoryManager.SubjectRepository.Create(DbDataConfigurator.Subjects);
            repositoryManager.TeacherRepository.Create(DbDataConfigurator.Teachers);

            await context.Response.WriteAsync("Done");
        }
        else
        {
            await context.Response.WriteAsync("Data is already exists");
        }
    });
});

app.Run();

